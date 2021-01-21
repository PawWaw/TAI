using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using Backend.RestModel;
using Backend.Helpers;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly DragorantContext _context;

        public OwnersController(DragorantContext context)
        {
            _context = context;
        }

        // GET: api/Owners/user
        // Information about current logged in user
        [Authorize]
        [HttpGet("user")]
        public ActionResult<WsOwnerLoginResponse> GetOwner_Owner()
        {
            var ownerId = (long)HttpContext.Items["ownerId"];

            var user = _context.Owners.Include(o => o.City).Include(o=>o.Restaurant).FirstOrDefault(o => o.Id == ownerId);
            if(user == null)
            {
                return NotFound();
            }
            WsOwnerLoginResponse loginResponse = new WsOwnerLoginResponse
            {
                Token = JwtService.GenerateOwnerJwtToken(user),
                Username = user.Username,
                Address = user.Address,
                City = user.City.Name,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RestaurantName = user.Restaurant.Name
            };
            return Ok(loginResponse);
        }

        // PUT: api/Owners/user
        // Update user
        [Authorize]
        [HttpPut("user")]
        public async Task<IActionResult> PutOwner_Owner(WsPutUser wsUser)
        {
            var ownerId = (long)HttpContext.Items["ownerId"];

            var authUser = await _context.Owners.Include(o => o.City).Include(o=>o.Restaurant.OrderStation.City).FirstOrDefaultAsync(o => o.Id == ownerId);
            if(authUser==null)
            {
                return NotFound();
            }
            wsUser.FIllPutUser(authUser);

            _context.Entry(authUser).State = EntityState.Modified;
            _context.Entry(authUser.Restaurant.OrderStation).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(authUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        // PUT: api/Owners/user/password
        // Update password
        [Authorize]
        [HttpPut("user/password")]
        public async Task<IActionResult> PutPassword_Owner(WsPutPassword passwordPut)
        {
            var ownerId = (long)HttpContext.Items["ownerId"];

            var authUser = await _context.Owners.Include(o=>o.Restaurant.OrderStation).FirstOrDefaultAsync(o => o.Id == ownerId);
            if(authUser == null)
            {
                return NotFound();
            }
            if (authUser.Password != passwordPut.OldPassword.Password)
            {
                return ValidationProblem();
            }
            authUser.InsertHashedPassword(passwordPut.NewPassword.Password);
            authUser.Restaurant.OrderStation.InsertHashedPassword(passwordPut.NewPassword.Password);
            _context.Entry(authUser).State = EntityState.Modified;
            _context.Entry(authUser.Restaurant.OrderStation).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(authUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        // POST: api/Owners/user/register
        // Register
        [HttpPost("user/register")]
        public async Task<IActionResult> PostOwner([Bind("address,username,password,email,firstName,lastName,city")] WsOwnerStation user)
        {
            var dbOwner = await _context.Owners.FirstOrDefaultAsync(o => o.Username == user.Username);
            if(dbOwner!=null)
            {
                return StatusCode(409);
            }

            Owner newOwner = new Owner();
            Restaurant newRestaurant = new Restaurant();
            OrderStation newOrderStation = new OrderStation();
            newOwner.FillProperties(user);
            newRestaurant.Name = user.RestaurantName;
            newOrderStation.FillProperties(user);
            newRestaurant.Owner = newOwner;
            newRestaurant.OwnerId = newOwner.Id;
            newRestaurant.OrderStation = newOrderStation;

            if (user.City != null && user.City.Length > 0)
            {
                City temp = _context.Cities.Where(c => c.Name == user.City).SingleOrDefault();
                if (temp == null)
                {
                    temp = new City
                    {
                        Name = user.City
                    };
                    _context.Cities.Add(temp);
                }
                newOwner.CityId = temp.Id;
                newOwner.City = temp;
                newOrderStation.City = temp;
                newOrderStation.CityId = temp.Id;
            }
            _context.Owners.Add(newOwner);
            _context.OrderStations.Add(newOrderStation);
            _context.Restaurants.Add(newRestaurant);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        // POST: api/Owners/auth
        // Authorization
        [HttpPost("auth")]
        public async Task<ActionResult<WsLoginResponse>> Login(LoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var findUser = await _context.Owners.Include(u => u.City).Include(u=>u.Restaurant)
                    .FirstOrDefaultAsync(m => m.Username == user.Username && m.Password == user.Password);
                if (findUser != null)
                {
                    WsOwnerLoginResponse loginResponse = new WsOwnerLoginResponse
                    {
                        Token = JwtService.GenerateOwnerJwtToken(findUser),
                        Username = findUser.Username,
                        Address = findUser.Address,
                        City = findUser.City.Name,
                        Email = findUser.Email,
                        FirstName = findUser.FirstName,
                        LastName = findUser.LastName,
                        RestaurantName = findUser.Restaurant.Name
                    };
                    return Ok(loginResponse);
                }
            }
            return StatusCode(401);
        }

        // DELETE: api/Owners/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Owner>> DeleteOwner(long id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return owner;
        }

        private bool OwnerExists(long id)
        {
            return _context.Owners.Any(e => e.Id == id);
        }
    }
}

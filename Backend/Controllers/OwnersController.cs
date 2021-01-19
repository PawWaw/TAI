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
        public ActionResult<LoginResponse> GetOwner()
        {
            var ownerId = (long)HttpContext.Items["ownerId"];

            var user = _context.Owners.Include(o => o.City).FirstOrDefault(o => o.Id == ownerId);
            if(user == null)
            {
                return NotFound();
            }
            LoginResponse loginResponse = new LoginResponse
            {
                Token = JwtService.GenerateOwnerJwtToken(user),
                Username = user.Username,
                Address = user.Address,
                City = user.City.Name,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return Ok(loginResponse);
        }

        // PUT: api/Owners/user
        // Update user
        [Authorize]
        [HttpPut("user")]
        public async Task<IActionResult> PutOwner(WsPutUser wsUser)
        {
            var ownerId = (long)HttpContext.Items["ownerId"];

            var authUser = await _context.Owners.Include(o => o.City).FirstAsync(o => o.Id == ownerId);
            wsUser.FIllPutUser(authUser);
            _context.Entry(authUser).State = EntityState.Modified;

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
        public async Task<IActionResult> PutPassword(WsPutPassword passwordPut)
        {
            var ownerId = (long)HttpContext.Items["ownerId"];

            var authUser = await _context.Owners.FirstAsync(o => o.Id == ownerId);
            if (authUser.Password == passwordPut.OldPassword.Value)
            {
                authUser.InsertHashedPassword(passwordPut.NewPassword.Value);
            }
            _context.Entry(authUser).State = EntityState.Modified;

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
        // POST: api/Owners
        // Register
        [HttpPost]
        public async Task<IActionResult> PostOwner([Bind("address,username,password,email,firstName,lastName,city")] WsUser user)
        {
            Owner newOwner = new Owner();
            newOwner.FillProperties(user);
            var dbOwner = _context.Owners.FirstOrDefaultAsync(o => o.Username == newOwner.Username);
            if(dbOwner!=null)
            {
                return StatusCode(409);
            }
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
            }
            _context.Owners.Add(newOwner);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Owners/auth
        // Authorization
        [HttpPost("auth")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var findUser = await _context.Owners.Include(u => u.City)
                    .FirstOrDefaultAsync(m => m.Username == user.Username && m.Password == user.Value);
                if (findUser != null)
                {
                    LoginResponse loginResponse = new LoginResponse
                    {
                        Token = JwtService.GenerateOwnerJwtToken(findUser),
                        Username = findUser.Username,
                        Address = findUser.Address,
                        City = findUser.City.Name,
                        Email = findUser.Email,
                        FirstName = findUser.FirstName,
                        LastName = findUser.LastName
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

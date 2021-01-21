using System.Collections.Generic;
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
    public class UsersController : ControllerBase
    {
        private readonly DragorantContext _context;
        public UsersController(DragorantContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/user
        // Information about current logged in user
        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<WsLoginResponse>> GetUserAsync_User()
        {
            var userId = (long)HttpContext.Items["userId"];

            var user = await _context.Users.Include(u => u.City).FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null)
            {
                return NotFound();
            }
            WsLoginResponse loginResponse = new WsLoginResponse
            {
                Token = JwtService.GenerateUserJwtToken(user),
                Username = user.Username.Trim(),
                Address = user.Address.Trim(),
                City = user.City.Name,
                Email = user.Email.Trim(),
                FirstName = user.FirstName.Trim(),
                LastName = user.LastName.Trim()
            };
            return Ok(loginResponse);
        }

        // PUT: api/Users/user
        // Update user
        [Authorize]
        [HttpPut("user")]
        public async Task<IActionResult> PutUser_User(WsPutUser wsUser)
        {
            var userId = (long)HttpContext.Items["userId"];

            var authUser = await _context.Users.Include(u => u.City).FirstAsync(u => u.Id == userId);
            wsUser.FIllPutUser(authUser);
            _context.Entry(authUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(authUser.Id))
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
        // PUT: api/Users/password
        // Update password
        [Authorize]
        [HttpPut("password")]
        public async Task<IActionResult> PutPassword(WsPutPassword passwordPut)
        {
            var userId = (long)HttpContext.Items["userId"];

            var authUser = await _context.Users.FirstAsync(u=>u.Id == userId);
            if (authUser.Password != passwordPut.OldPassword.Password)
            {
                return ValidationProblem();
            }
            authUser.InsertHashedPassword(passwordPut.NewPassword.Password);
            _context.Entry(authUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(authUser.Id))
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
        // POST: api/Users/user/register
        // Register
        [HttpPost("user/register")]
        public async Task<IActionResult> PostUser([Bind("address,username,password,email,firstName,lastName,city")] WsUser user)
        {
            User newUser = new User();
            newUser.FillProperties(user);
            var dbUser = await _context.Deliverers.FirstOrDefaultAsync(o => o.Username == newUser.Username);
            if (dbUser != null)
            {
                return StatusCode(409);
            }
            if (user.City !=null && user.City.Length > 0)
            {
                City temp = _context.Cities.Where(c => c.Name == user.City).SingleOrDefault();
                if(temp == null)
                {
                    temp = new City
                    {
                        Name = user.City
                    };
                    _context.Cities.Add(temp);
                }
                newUser.CityId = temp.Id;
                newUser.City = temp;
            }
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        // POST: api/Users/auth
        // Authorization
        [HttpPost("login")]
        public async Task<ActionResult<WsLoginResponse>> Login(LoginRequest user)
        {
            if(ModelState.IsValid)
            {
                var findUser = await _context.Users.Include(u=>u.City)
                    .FirstOrDefaultAsync(m => m.Username == user.Username && m.Password == user.Password);
                if (findUser != null)
                {
                    WsLoginResponse loginResponse = new WsLoginResponse
                    {
                        Token = JwtService.GenerateUserJwtToken(findUser),
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

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
       
    }
}

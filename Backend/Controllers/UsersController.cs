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
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [Authorize]
        [HttpGet("user")]
        public ActionResult<User> GetUser()
        {
            var user = (User)HttpContext.Items["User"];
            LoginResponse loginResponse = new LoginResponse
            {
                Token = JwtService.GenerateJwtToken(user.Id),
                Username = user.Username,
                Address = user.Address,
                City = user.City.Name,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return Ok(loginResponse);
            //return Ok();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // Register
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([Bind("address,username,password,email,firstName,lastName,city")] WsUser user)
        {
            User dbUser = new User();
            dbUser.FillProperties(user);
            if(user.City !=null && user.City.Length > 0)
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
                dbUser.CityId = temp.Id;
                dbUser.City = temp;
            }
            _context.Users.Add(dbUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = dbUser.Id }, user);
            //return Ok();
        }

        // POST: api/Users/auth
        // Authorization
        [HttpPost("auth")]
        public async Task<ActionResult<User>> Login(LoginRequest user)
        {
            if(ModelState.IsValid)
            {
                var findUser = await _context.Users.Include(u=>u.City)
                    .FirstOrDefaultAsync(m => m.Username == user.Username && m.Password == user.Password);
                if (findUser != null)
                {
                    LoginResponse loginResponse = new LoginResponse
                    {
                        Token = JwtService.GenerateJwtToken(findUser.Id),
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

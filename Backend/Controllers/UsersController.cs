using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Backend.RestModel;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([Bind("address,username,password,email,firstName,lastName,city")] WsUser user)
        {
            User dbUser = new User();
            dbUser.fillProperties(user);
            if(user.City !=null && user.City.Length > 0)
            {
                City temp = _context.Cities.Where(c => c.Name == user.City).SingleOrDefault();
                if(temp == null)
                {
                    temp = new City();
                    temp.Name = user.City;
                    _context.Cities.Add(temp);
                }
                dbUser.CityId = temp.Id;
                dbUser.City = temp;
            }
            _context.Users.Add(dbUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = dbUser.Id }, user);
        }

        // POST: api/Users/auth
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("auth")]
        public async Task<ActionResult<User>> Login(LoginRequest user)
        {
            if(ModelState.IsValid)
            {
                var findUser = await _context.Users
                    .FirstOrDefaultAsync(m => m.Username == user.Username && m.Password == user.Password);
                if (findUser != null)
                {
                    return Ok(GenerateJwtToken((int)findUser.Id));
                }
            }
            return StatusCode(401); 
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
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
        public string GenerateJwtToken(int accountId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("[SECRET USED TO SIGN AND VERIFY JWT TOKENS, IT CAN BE ANY STRING]");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", accountId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

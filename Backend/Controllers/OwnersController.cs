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
            var userId = (long)HttpContext.Items["UserId"];
            var user = _context.Owners.Include(o => o.City).FirstOrDefault(o => o.Id == userId);
            if(user == null)
            {
                return NotFound();
            }
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
        }

        // PUT: api/Owners/user
        // Update user
        [Authorize]
        [HttpPut("user")]
        public async Task<IActionResult> PutOwner(WsPutUser wsUser)
        {
            var userId = (long)HttpContext.Items["UserId"];
            var authUser = await _context.Owners.Include(o => o.City).FirstAsync(o => o.Id == userId);
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
        // PUT: api/Owners/password
        // Update password
        [Authorize]
        [HttpPut("password")]
        public async Task<IActionResult> PutPassword(WsPasswordPut passwordPut)
        {
            var userId = (long)HttpContext.Items["UserId"];
            var authUser = await _context.Owners.FirstAsync(o => o.Id == userId);
            if (authUser.Password == passwordPut.OldPassword.Value)
            {
                authUser.Password = passwordPut.NewPassword.Value;
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
            Owner dbOwner = new Owner();
            dbOwner.FillProperties(user);
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
                dbOwner.CityId = temp.Id;
                dbOwner.City = temp;
            }
            _context.Owners.Add(dbOwner);
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

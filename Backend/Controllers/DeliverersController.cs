using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using Backend.RestModel;
using Backend.Helpers;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverersController : ControllerBase
    {
        private readonly DragorantContext _context;

        public DeliverersController(DragorantContext context)
        {
            _context = context;
        }

        // GET: /api/Deliverers/statistics
        [Authorize]
        [HttpGet("statistics")]
        public ActionResult<WsStatistics> GetStatistics()
        {
            var delivererId = (long)HttpContext.Items["UserId"];
            var deliverer = _context.Deliverers.Include(d=>d.DelivererRates).Include(d=>d.Orders).FirstOrDefault(o => o.Id == delivererId);
            WsStatistics statistics = new WsStatistics
            {
                ClientRate = deliverer.DelivererRates.Average(d => d.Value),
                TotalDelivery = deliverer.Orders.Count,
                CurrentOrders = deliverer.Orders.Where(o => o.Status != "ENDED").Count(),
                MaxDailyOrders = deliverer.Orders.GroupBy(o => o.EndTime.Date.ToString("d")).Count()
            };
            return Ok(statistics);
        }

        // GET: api/Deliverers/user
        // Information about current logged in user
        [Authorize]
        [HttpGet("user")]
        public ActionResult<LoginResponse> GetDeliverer()
        {
            var userId = (long)HttpContext.Items["UserId"];
            var user = _context.Deliverers.Include(d => d.City).FirstOrDefault(d => d.Id == userId);
            if (user == null)
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

        // PUT: api/Deliverers/user
        // Update user
        [Authorize]
        [HttpPut("user")]
        public async Task<IActionResult> PutDeliverer(WsPutUser wsUser)
        {
            var userId = (long)HttpContext.Items["UserId"];
            var authUser = await _context.Deliverers.Include(d => d.City).FirstAsync(d => d.Id == userId);
            wsUser.FIllPutUser(authUser);
            _context.Entry(authUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DelivererExists(authUser.Id))
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
        // PUT: api/Deliverers/user/password
        // Update password
        [Authorize]
        [HttpPut("user/password")]
        public async Task<IActionResult> PutPassword(WsPasswordPut passwordPut)
        {
            var userId = (long)HttpContext.Items["UserId"];
            var authUser = await _context.Deliverers.FirstAsync(d => d.Id == userId);
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
                if (!DelivererExists(authUser.Id))
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
        // POST: api/Deliverers/user/register
        // Register
        [HttpPost("user/register")]
        public async Task<IActionResult> PostDeliverer([Bind("address,username,password,email,firstName,lastName,city")] WsUser user)
        {
            Deliverer dbDeliverer = new Deliverer();
            dbDeliverer.FillProperties(user);
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
                dbDeliverer.CityId = temp.Id;
                dbDeliverer.City = temp;
            }
            _context.Deliverers.Add(dbDeliverer);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Deliverers/auth
        // Authorization
        [HttpPost("user/login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var findUser = await _context.Deliverers.Include(u => u.City)
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

        // DELETE: api/Deliverers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deliverer>> DeleteDeliverer(long id)
        {
            var deliverer = await _context.Deliverers.FindAsync(id);
            if (deliverer == null)
            {
                return NotFound();
            }

            _context.Deliverers.Remove(deliverer);
            await _context.SaveChangesAsync();

            return deliverer;
        }

        private bool DelivererExists(long id)
        {
            return _context.Deliverers.Any(e => e.Id == id);
        }
    }
}

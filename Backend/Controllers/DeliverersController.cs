using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using Backend.RestModel;
using Backend.Helpers;
using System.Collections.Generic;

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

        // GET: api/Deliverers
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deliverer>>> GetDeliverers_Owner()
        {
            return await _context.Deliverers.ToListAsync();
        }

        // GET: /api/Deliverers/statistics
        [Authorize]
        [HttpGet("statistics")]
        public ActionResult<WsStatistics> GetStatistics_Deliverer()
        {
            var delivererId = (long)HttpContext.Items["delivererId"];
            var deliverer = _context.Deliverers.Include(d=>d.DelivererRates).Include(d=>d.Orders).FirstOrDefault(o => o.Id == delivererId);
            if(deliverer == null)
            {
                return NotFound();
            }
            double tempClientRate = 0;
            int tempTotalDelivery = 0;
            int tempCurrentOrders = 0;
            int tempMaxDailyOrders = 0;
            if (deliverer.DelivererRates.Count() != 0)
            {
                tempClientRate = deliverer.DelivererRates.Average(d => d.Value);
            }
            if(deliverer.Orders != null)
            {
                tempTotalDelivery = deliverer.Orders.Count;
                if (deliverer.Orders.Where(o => o.Status.TrimEnd() != "ENDED").Count() != 0)
                {
                    tempCurrentOrders = deliverer.Orders.Where(o => o.Status.TrimEnd() != "ENDED").Count();
                }
                if (deliverer.Orders.Where(o => o.Status.TrimEnd() == "ENDED").Count() != 0)
                {
                    tempMaxDailyOrders = deliverer.Orders.Where(o => o.Status.TrimEnd() == "ENDED")
                                        .GroupBy(o => o.EndTime.Value.ToString("s"))
                                        .Max(gr => gr.Count());
                }
            }
            
            WsStatistics statistics = new WsStatistics
            {
                ClientRate = tempClientRate,
                TotalDelivery = tempTotalDelivery,
                CurrentOrders = tempCurrentOrders,
                MaxDailyOrders = tempMaxDailyOrders
            };
            return Ok(statistics); 
        }


        // GET: api/Deliverers/user
        // Information about current logged in user
        [Authorize]
        [HttpGet("user")]
        public ActionResult<WsDelivererLoginResponse> GetDeliverer_Deliverer()
        {
            var delivererId = (long)HttpContext.Items["delivererId"];

            var user = _context.Deliverers.Include(d => d.City).FirstOrDefault(d => d.Id == delivererId);
            if (user == null)
            {
                return NotFound();
            }

            WsDelivererLoginResponse loginResponse = new WsDelivererLoginResponse
            {
                Token = JwtService.GenerateDelivererJwtToken(user),
                Username = user.Username,
                Address = user.Address,
                City = user.City.Name,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DragonCoinBalance = user.DragonCoinBalance
            };
            return Ok(loginResponse);
        }


        // PUT: api/Deliverers/user
        // Update user
        [Authorize]
        [HttpPut("user")]
        public async Task<IActionResult> PutDeliverer_Deliverer(WsPutUser wsUser)
        {
            var delivererId = (long)HttpContext.Items["delivererId"];

            var authUser = await _context.Deliverers.Include(d => d.City).FirstOrDefaultAsync(d => d.Id == delivererId);
            if(authUser == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> PutPassword_Deliverer(WsPutPassword passwordPut)
        {
            var delivererId = (long)HttpContext.Items["delivererId"];

            var authUser = await _context.Deliverers.FirstOrDefaultAsync(d => d.Id == delivererId);
            if(authUser == null)
            {
                return NotFound();
            }
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
            Deliverer newDeliverer = new Deliverer();
            newDeliverer.FillProperties(user);
            var dbDeliverer = await _context.Deliverers.FirstOrDefaultAsync(o => o.Username == newDeliverer.Username);
            if (dbDeliverer != null)
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
                newDeliverer.CityId = temp.Id;
                newDeliverer.City = temp;
            }
            _context.Deliverers.Add(newDeliverer);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        // POST: api/Deliverers/auth
        // Authorization
        [HttpPost("user/login")]
        public async Task<ActionResult<WsDelivererLoginResponse>> Login(LoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var findUser = await _context.Deliverers.Include(u => u.City)
                    .FirstOrDefaultAsync(m => m.Username == user.Username && m.Password == user.Password);
                if (findUser != null)
                {
                    WsDelivererLoginResponse loginResponse = new WsDelivererLoginResponse
                    {
                        Token = JwtService.GenerateDelivererJwtToken(findUser),
                        Username = findUser.Username,
                        Address = findUser.Address,
                        City = findUser.City.Name,
                        Email = findUser.Email,
                        FirstName = findUser.FirstName,
                        LastName = findUser.LastName,
                        DragonCoinBalance = findUser.DragonCoinBalance
                    };
                    return Ok(loginResponse);
                }
            }
            return StatusCode(401);
        }

        // DELETE: api/Deliverers/5
        [HttpDelete("{id}")]
        [Authorize]
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using Backend.Helpers;
using Backend.RestModel;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStationsController : ControllerBase
    {
        private readonly DragorantContext _context;

        public OrderStationsController(DragorantContext context)
        {
            _context = context;
        }

        // GET: api/OrderStations/user
        // Information about a current logged in user
        [Authorize]
        [HttpGet("user")]
        public ActionResult<WsStationLoginResponse> GetOrderStation()
        {
            var stationId = GetStationIdFromContext();
            if (stationId <= 0)
            {
                return StatusCode(409);
            }
            var user = _context.OrderStations.Include(o => o.City).FirstOrDefault(o => o.Id == stationId);
            if (user == null)
            {
                return NotFound();
            }
            WsStationLoginResponse loginResponse = new WsStationLoginResponse
            {
                Token = JwtService.GenerateOrderStationJwtToken(user),
                Username = user.Username,
                Address = user.Address,
                City = user.City.Name,
            };
            return Ok(loginResponse);
        }

        // PUT: api/OrderStations/user
        // Update Station
        [Authorize]
        [HttpPut("user")]
        public async Task<IActionResult> PutOrderStation(WsPutUser wsUser)
        {
            var stationId = GetStationIdFromContext();
            if(stationId <= 0)
            {
                return StatusCode(409);
            }
            var authUser = await _context.OrderStations.Include(o => o.City).FirstAsync(o => o.Id == stationId);
            wsUser.FIllPutUser(authUser);
            _context.Entry(authUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStationExists(authUser.Id))
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
        // PUT: api/OrderStations/user/password
        // Update password
        [Authorize]
        [HttpPut("user/password")]
        public async Task<IActionResult> PutPassword(WsPutPassword passwordPut)
        {
            var stationId = GetStationIdFromContext();
            if (stationId <= 0)
            {
                return StatusCode(409);
            }
            var authUser = await _context.OrderStations.FirstAsync(o => o.Id == stationId);
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
                if (!OrderStationExists(authUser.Id))
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
        // POST: api/OrderStations
        // Register
        [HttpPost]
        public async Task<IActionResult> PostOrderStation([Bind("address,username,password, city")] WsStation user)
        {
            OrderStation newStation = new OrderStation();
            newStation.FillProperties(user);
            var dbStation = await _context.OrderStations.FirstOrDefaultAsync(o => o.Username == newStation.Username);
            if (dbStation != null)
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
                newStation.CityId = temp.Id;
                newStation.City = temp;
            }
            _context.OrderStations.Add(newStation);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/OrderStations/auth
        // Authorization
        [HttpPost("auth")]
        public async Task<ActionResult<WsStationLoginResponse>> Login(LoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var findUser = await _context.OrderStations.Include(u => u.City)
                    .FirstOrDefaultAsync(m => m.Username == user.Username && m.Password == user.Value);
                if (findUser != null)
                {
                    WsStationLoginResponse loginResponse = new WsStationLoginResponse
                    {
                        Token = JwtService.GenerateOrderStationJwtToken(findUser),
                        Username = findUser.Username,
                        Address = findUser.Address,
                        City = findUser.City.Name,
                    };
                    return Ok(loginResponse);
                }
            }
            return StatusCode(401);
        }

        // DELETE: api/OrderStations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderStation>> DeleteOrderStation(long id)
        {
            var orderStation = await _context.OrderStations.FindAsync(id);
            if (orderStation == null)
            {
                return NotFound();
            }

            _context.OrderStations.Remove(orderStation);
            await _context.SaveChangesAsync();

            return orderStation;
        }

        private bool OrderStationExists(long id)
        {
            return _context.OrderStations.Any(e => e.Id == id);
        }

        private long GetStationIdFromContext()
        {
            var stationId = 0L;
            if (HttpContext.Items["stationId"] != null)
            {
                stationId = (long)HttpContext.Items["stationId"];
            }
            return stationId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;

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

        // GET: api/OrderStations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStation>>> GetOrderStations()
        {
            return await _context.OrderStations.ToListAsync();
        }

        // GET: api/OrderStations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStation>> GetOrderStation(long id)
        {
            var orderStation = await _context.OrderStations.FindAsync(id);

            if (orderStation == null)
            {
                return NotFound();
            }

            return orderStation;
        }

        // PUT: api/OrderStations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderStation(long id, OrderStation orderStation)
        {
            if (id != orderStation.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderStation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStationExists(id))
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

        // POST: api/OrderStations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderStation>> PostOrderStation(OrderStation orderStation)
        {
            _context.OrderStations.Add(orderStation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderStation", new { id = orderStation.Id }, orderStation);
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
    }
}

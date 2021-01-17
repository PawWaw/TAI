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
    public class OrdersController : ControllerBase
    {
        private readonly DragorantContext _context;

        public OrdersController(DragorantContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(long id)
        {
            var order = await _context.Orders.Include(e => e.OrderStation).Include(e => e.Deliverer).Include(e => e.User).Include(e => e.User.City).SingleOrDefaultAsync(e => e.Id == id); ;

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(long id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(long id)
        {
            var order = await _context.Orders.Include(e => e.OrderStation).Include(e => e.Deliverer).Include(e=>e.User).Include(e=>e.User.City).SingleOrDefaultAsync(e=> e.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        [HttpGet("isCurrent")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders([FromQuery] bool current)
        {
            if (current)
            {
                return await _context.Orders.Where(e => e.Status != "ENDED").Include(e => e.OrderStation).Include(e => e.Deliverer).Include(e => e.User).Include(e => e.User.City).ToListAsync();
            }
            else
            {
                return await _context.Orders.Where(e => e.Status == "ENDED").Include(e => e.OrderStation).Include(e => e.Deliverer).Include(e => e.User).Include(e => e.User.City).ToListAsync();
            }

        }

        [HttpGet("find")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersToRealise([FromQuery] double latitude, [FromQuery] double longitude)
        {
            //do dokonczenia
                return await _context.Orders.Where(e => e.Status != "ENDED").ToListAsync();

        }

        [HttpPost("take")]
        public async Task<ActionResult<Order>> RealizeOrder(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = "REALIZE";
            await _context.SaveChangesAsync();

            return order;
        }

        [HttpPost("delivered")]
        public async Task<ActionResult<Order>> DeliverOrder(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = "ENDED";
            order.EndTime = DateTime.Now; //mozliwe ze trzeba bedzie w bazie zmienic wartosc na datetime2 jesli nie ma teraz
            await _context.SaveChangesAsync();

            return order;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Order>> PatchOrderDelivery(long id, long deliverer)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            order.DelivererId = deliverer;
            await _context.SaveChangesAsync();

            return order;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Order>> PatchOrder(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }
        private bool OrderExists(long id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}

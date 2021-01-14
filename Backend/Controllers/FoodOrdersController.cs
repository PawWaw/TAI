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
    public class FoodOrdersController : ControllerBase
    {
        private readonly DragorantContext _context;

        public FoodOrdersController(DragorantContext context)
        {
            _context = context;
        }

        // GET: api/FoodOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodOrder>>> GetFoodOrders()
        {
            return await _context.FoodOrders.ToListAsync();
        }

        // GET: api/FoodOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodOrder>> GetFoodOrder(long id)
        {
            var foodOrder = await _context.FoodOrders.FindAsync(id);

            if (foodOrder == null)
            {
                return NotFound();
            }

            return foodOrder;
        }

        // PUT: api/FoodOrders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodOrder(long id, FoodOrder foodOrder)
        {
            if (id != foodOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(foodOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodOrderExists(id))
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

        // POST: api/FoodOrders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FoodOrder>> PostFoodOrder(FoodOrder foodOrder)
        {
            _context.FoodOrders.Add(foodOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodOrder", new { id = foodOrder.Id }, foodOrder);
        }

        // DELETE: api/FoodOrders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FoodOrder>> DeleteFoodOrder(long id)
        {
            var foodOrder = await _context.FoodOrders.FindAsync(id);
            if (foodOrder == null)
            {
                return NotFound();
            }

            _context.FoodOrders.Remove(foodOrder);
            await _context.SaveChangesAsync();

            return foodOrder;
        }

        private bool FoodOrderExists(long id)
        {
            return _context.FoodOrders.Any(e => e.Id == id);
        }
    }
}

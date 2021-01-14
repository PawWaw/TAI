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
    public class FoodIngredientsController : ControllerBase
    {
        private readonly DragorantContext _context;

        public FoodIngredientsController(DragorantContext context)
        {
            _context = context;
        }

        // GET: api/FoodIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodIngredient>>> GetFoodIngredients()
        {
            return await _context.FoodIngredients.ToListAsync();
        }

        // GET: api/FoodIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodIngredient>> GetFoodIngredient(long id)
        {
            var foodIngredient = await _context.FoodIngredients.FindAsync(id);

            if (foodIngredient == null)
            {
                return NotFound();
            }

            return foodIngredient;
        }

        // PUT: api/FoodIngredients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodIngredient(long id, FoodIngredient foodIngredient)
        {
            if (id != foodIngredient.Id)
            {
                return BadRequest();
            }

            _context.Entry(foodIngredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodIngredientExists(id))
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

        // POST: api/FoodIngredients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FoodIngredient>> PostFoodIngredient(FoodIngredient foodIngredient)
        {
            _context.FoodIngredients.Add(foodIngredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodIngredient", new { id = foodIngredient.Id }, foodIngredient);
        }

        // DELETE: api/FoodIngredients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FoodIngredient>> DeleteFoodIngredient(long id)
        {
            var foodIngredient = await _context.FoodIngredients.FindAsync(id);
            if (foodIngredient == null)
            {
                return NotFound();
            }

            _context.FoodIngredients.Remove(foodIngredient);
            await _context.SaveChangesAsync();

            return foodIngredient;
        }

        private bool FoodIngredientExists(long id)
        {
            return _context.FoodIngredients.Any(e => e.Id == id);
        }
    }
}

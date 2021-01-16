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
    public class FoodsController : ControllerBase
    {
        private readonly DragorantContext _context;

        public FoodsController(DragorantContext context)
        {
            _context = context;
        }

        // GET: api/Foods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
        {
            return await _context.Foods.Include(f=>f.FoodIngredients).ToListAsync();
        }

        // GET: api/Foods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(long id)
        {
            var food = await _context.Foods.FindAsync(id);

            if (food == null)
            {
                return NotFound();
            }

            return food;
        }

        // PUT: api/Foods/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood(long id, Food food)
        {
            if (id != food.Id)
            {
                return BadRequest();
            }

            _context.Entry(food).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
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

        // POST: api/Foods
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Dish dish)
        {
            Food food = new Food();
            food.inject_data(dish);
            Ingredient temp_ingredient;
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            foreach (string ingredient in dish.ingredients)
            {
                temp_ingredient = await _context.Ingredients.FirstOrDefaultAsync(e => e.Name == ingredient);
                if (temp_ingredient == null)
                {
                    Ingredient new_ingredient = new Ingredient();
                    new_ingredient.Name = ingredient;
                    _context.Ingredients.Add(new_ingredient);
                    await _context.SaveChangesAsync();
                    temp_ingredient = new_ingredient;
                }
                FoodIngredient temp_foodIngredient = new FoodIngredient();
                temp_foodIngredient.FoodId = food.Id;
                temp_foodIngredient.IngredientId = temp_ingredient.Id;
                _context.FoodIngredients.Add(temp_foodIngredient);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetFood", new { id = food.Id }, food);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Food>> PatchFood(long id, Dish dish)
        {
            List<Ingredient> ingredient_list = new List<Ingredient>();
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            food.Name = dish.name;
            food.Price = dish.price;
            foreach(string temp_ingredient_name in dish.ingredients)
            {
                var ingredient = await _context.Ingredients.FindAsync(temp_ingredient_name);
                if(ingredient == null)
                {
                    Ingredient temp_ingredient = new Ingredient();
                    temp_ingredient.Name = temp_ingredient_name;
                    _context.Ingredients.Add(temp_ingredient);
                    await _context.SaveChangesAsync();
                    ingredient = temp_ingredient;
                }
                if (await _context.FoodIngredients.FirstOrDefaultAsync(e => e.FoodId == food.Id && e.IngredientId == ingredient.Id) == null)
                {
                    FoodIngredient temp_foodIngredient = new FoodIngredient();
                    temp_foodIngredient.FoodId = food.Id;
                    temp_foodIngredient.IngredientId = ingredient.Id;
                    _context.FoodIngredients.Add(temp_foodIngredient);
                    await _context.SaveChangesAsync();
                }
                ingredient_list.Add(ingredient);
            }
            foreach(FoodIngredient temp in food.FoodIngredients)
            {
                if (!ingredient_list.Contains(temp.Ingredient))
                {
                    _context.FoodIngredients.Remove(temp);
                    await _context.SaveChangesAsync();
                }
            }

            return food;
        }

            // DELETE: api/Foods/5
            [HttpDelete("{id}")]
        public async Task<ActionResult<Food>> DeleteFood(long id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();

            return food;
        }

        private bool FoodExists(long id)
        {
            return _context.Foods.Any(e => e.Id == id);
        }
    }
}

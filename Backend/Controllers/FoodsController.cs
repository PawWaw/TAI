using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using Backend.Helpers;

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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dish>>> GetFoods_Owner()
        {
            long ownerId = (long)HttpContext.Items["ownerId"];
            List<Dish> dish_list = new List<Dish>();
            Owner owner = await _context.Owners.Include(o=>o.Restaurant.Foods).FirstOrDefaultAsync(o=>o.Id == ownerId);
            if(owner == null)
            {
                return Unauthorized();
            }
            var foods = owner.Restaurant.Foods.ToList();

            IEnumerable<Food> list_food = await _context.Foods.Include(f=>f.FoodIngredients).Where(f=>(bool)f.IsActive == true).ToListAsync();
            foreach(Food food in list_food)
            {
                if (foods.Select(f=>f.Id).Contains(food.Id))
                {
                    Dish temp = new Dish
                    {
                        id = food.Id,
                        name = food.Name,
                        price = food.Price,
                        ingredients = new string[food.FoodIngredients.Count()]
                    };
                    int i = 0;
                    foreach (FoodIngredient temp_ingredient in food.FoodIngredients)
                    {
                        var temp_foodingredient = await _context.FoodIngredients.Include(t => t.Ingredient).SingleOrDefaultAsync(t => t.Id == temp_ingredient.Id);
                        temp.ingredients[i] = (temp_foodingredient.Ingredient.Name.Trim());
                        i++;
                    }
                    dish_list.Add(temp);
                }
            }
            return dish_list;
        }

        // GET: api/Foods/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Dish>> GetFood_Owner(long id)
        {
            long ownerId = (long)HttpContext.Items["ownerId"];
            Owner user = await _context.Owners.Include(o=>o.Restaurant.Foods).FirstOrDefaultAsync(o=>o.Id == ownerId);
            if(user == null)
            {
                return NotFound();
            }
            Food foodNoIngredients = user.Restaurant.Foods.FirstOrDefault(f => f.Id == id && f.IsActive == true);
            if (foodNoIngredients == null || await _context.Foods.ContainsAsync(foodNoIngredients) == false)
            {
                return NotFound();
            }
            Food food = await _context.Foods.Include(f => f.FoodIngredients).FirstOrDefaultAsync(f => f.Id == foodNoIngredients.Id);
            Dish dish = new Dish
            {
                id = food.Id,
                name = food.Name,
                price = food.Price,
                ingredients = new string[food.FoodIngredients.Count()]
            };
            int i = 0;
            foreach (FoodIngredient temp_ingredient in food.FoodIngredients)
            {
                var temp_foodingredient = await _context.FoodIngredients.Include(t => t.Ingredient).SingleOrDefaultAsync(t => t.Id == temp_ingredient.Id);
                dish.ingredients[i] = (temp_foodingredient.Ingredient.Name.Trim());
                i++;
            }
            return dish;
        }

        // PUT: api/Foods/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
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
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood_Owner(Dish dish)
        {
            long ownerId = (long)HttpContext.Items["ownerId"];

            Food food = new Food();
            food.inject_data(dish);
            Ingredient temp_ingredient;
            food.IsActive = true;
            Owner user = await _context.Owners.Include(o=>o.Restaurant).Where(f => f.Id == ownerId).FirstOrDefaultAsync();
            if(user == null)
            {
                return NotFound();
            }
            food.IdRestaurant = user.Restaurant.Id;
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();

            foreach (string ingredient in dish.ingredients)
            {
                temp_ingredient = await _context.Ingredients.FirstOrDefaultAsync(e => e.Name == ingredient);
                if (temp_ingredient == null)
                {
                    Ingredient new_ingredient = new Ingredient
                    {
                        Name = ingredient
                    };
                    _context.Ingredients.Add(new_ingredient);
                    await _context.SaveChangesAsync();
                    temp_ingredient = new_ingredient;
                }
                FoodIngredient temp_foodIngredient = new FoodIngredient
                {
                    FoodId = food.Id,
                    IngredientId = temp_ingredient.Id
                };
                _context.FoodIngredients.Add(temp_foodIngredient);
                await _context.SaveChangesAsync();
            }
            return StatusCode(200);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<Food>> PatchFood_Owner(long id, Dish dish)
        {
            List<Ingredient> ingredient_list = new List<Ingredient>();
            var food = await _context.Foods.Include(x => x.FoodIngredients).FirstOrDefaultAsync(x => x.Id == id);
            if (food == null)
            {
                return NotFound();
            }
            food.Name = dish.name;
            food.Price = dish.price;
            await _context.SaveChangesAsync();
            foreach (string temp_ingredient_name in dish.ingredients)
            {
                var ingredient = await _context.Ingredients.FirstOrDefaultAsync(e => e.Name.Trim() == temp_ingredient_name.Trim());
                if(ingredient == null)
                {
                    Ingredient temp_ingredient = new Ingredient
                    {
                        Name = temp_ingredient_name
                    };
                    _context.Ingredients.Add(temp_ingredient);
                    await _context.SaveChangesAsync();
                    ingredient = temp_ingredient;
                }
                if (await _context.FoodIngredients.FirstOrDefaultAsync(e => e.FoodId == food.Id && e.IngredientId == ingredient.Id) == null)
                {
                    FoodIngredient temp_foodIngredient = new FoodIngredient
                    {
                        FoodId = food.Id,
                        IngredientId = ingredient.Id
                    };
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
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Food>> DeleteFood_Owner(long id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            food.IsActive = false;
            await _context.SaveChangesAsync();

            return food;
        }

        private bool FoodExists(long id)
        {
            return _context.Foods.Any(e => e.Id == id);
        }
    }
}

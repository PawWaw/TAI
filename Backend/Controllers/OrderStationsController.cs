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
    public class AverageRateFood
    {
        public Food food { get; set; }
        public double averageRate { get; set; }
    }
    public class BodyOrderStation
    {
        public OrderStation orderStation {get;set;}
        public List<AverageRateFood> foods { get; set; }
    }

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
            return await _context.OrderStations.Include(e=>e.Resteurant).ToListAsync();
        }

        [HttpGet("DishesFromOrderStations")]
        public async Task<ActionResult<IEnumerable<BodyOrderStation>>> GetDishOrderStations()
        {
            IEnumerable<OrderStation> orderStations = await _context.OrderStations.Include(e => e.Resteurant).Include(e =>e.Resteurant.Foods).ToListAsync();
            List<BodyOrderStation> bodyOrderStations = new List<BodyOrderStation>();
            foreach(OrderStation orderStation in orderStations)
            {
                BodyOrderStation temp = new BodyOrderStation();
                temp.foods = new List<AverageRateFood>();
                temp.orderStation = orderStation;
                foreach(Food food in orderStation.Resteurant.Foods)
                {
                    AverageRateFood tempFood = new AverageRateFood();
                    tempFood.food = await _context.Foods.Include(e => e.FoodRates).Include(e => e.FoodIngredients).FirstOrDefaultAsync(e => e.Id == food.Id);
                    List<FoodIngredient> tempIngredient = new List<FoodIngredient>();
                    foreach (FoodIngredient foodIngredient in tempFood.food.FoodIngredients)
                    {
                         FoodIngredient ingredient = await _context.FoodIngredients.Include(e => e.Ingredient).SingleOrDefaultAsync(e => e.Id == foodIngredient.Id);
                         tempIngredient.Add(ingredient);
                    }
                    tempFood.food.FoodIngredients = tempIngredient;
                    double i = 0;
                    double sum = 0;
                    foreach(FoodRate tempRate in tempFood.food.FoodRates)
                    {
                        i++;
                        sum += tempRate.Value;
                    }
                    tempFood.averageRate = sum / i;
                    temp.foods.Add(tempFood);
                }
                temp.orderStation.Resteurant.Foods = null;
                bodyOrderStations.Add(temp);
            }
            return bodyOrderStations;
        }

        [HttpGet("DishesFromOrderStations/{id}")]
        public async Task<ActionResult<BodyOrderStation>> GetDishOrderStations([FromQuery]long OrderStationId)
        {
            OrderStation orderStation = await _context.OrderStations.Include(e => e.Resteurant).Include(e => e.Resteurant.Foods).FirstOrDefaultAsync(e => e.Id == OrderStationId);
            BodyOrderStation bodyOrderStations = new BodyOrderStation();
            bodyOrderStations.foods = new List<AverageRateFood>();
            bodyOrderStations.orderStation = orderStation;
            foreach (Food food in orderStation.Resteurant.Foods)
            {
                AverageRateFood tempFood = new AverageRateFood();
                tempFood.food = await _context.Foods.Include(e => e.FoodRates).Include(e => e.FoodIngredients).FirstOrDefaultAsync(e => e.Id == food.Id);
                List<FoodIngredient> tempIngredient = new List<FoodIngredient>();
                foreach (FoodIngredient foodIngredient in tempFood.food.FoodIngredients)
                {
                    FoodIngredient ingredient = await _context.FoodIngredients.Include(e => e.Ingredient).SingleOrDefaultAsync(e => e.Id == foodIngredient.Id);
                    tempIngredient.Add(ingredient);
                }
                tempFood.food.FoodIngredients = tempIngredient;
                double i = 0;
                double sum = 0;
                foreach (FoodRate tempRate in tempFood.food.FoodRates)
                {
                    i++;
                    sum += tempRate.Value;
                }
                tempFood.averageRate = sum / i;
                bodyOrderStations.foods.Add(tempFood);
            }
            bodyOrderStations.orderStation.Resteurant.Foods = null;
            return bodyOrderStations;
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

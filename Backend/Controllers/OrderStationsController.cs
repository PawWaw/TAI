using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using Backend.Helpers;
using Backend.RestModel;
using System.Collections.Generic;
using Newtonsoft.Json;

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

    [JsonObject("OrderStation")]
    public class WsOrderStation
    {
        public long Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public WsRestaurant WsRestaurant { get; set; }
    }

    [JsonObject("Restaurant")]
    public class WsRestaurant
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<WsDishWithRate> WsDishWithRates { get; set; }
    }

    [JsonObject("DishWithRate")]
    public class WsDishWithRate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<string> Ingredients { get; set; }
        public double Rate { get; set; }
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

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStation>>> GetOrderStations_User()
        {
            return await _context.OrderStations.ToListAsync();
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

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("DishFromOrderStations")]
        public async Task<ActionResult<IEnumerable<WsOrderStation>>> GetDishesFromOrderStations_User()
        {
            IEnumerable<OrderStation> orderStations = await _context.OrderStations.Include(o => o.City).ToListAsync();
            List<BodyOrderStation> bodyOrderStations = new List<BodyOrderStation>();
            foreach (OrderStation orderStation in orderStations)
            {
                BodyOrderStation temp = new BodyOrderStation
                {
                    foods = new List<AverageRateFood>(),
                    orderStation = orderStation
                };
                Restaurant restaurant = await _context.Restaurants.Where(f => f.Id == orderStation.RestaurantId).Include(e => e.Foods).FirstOrDefaultAsync();
                orderStation.Restaurant = restaurant;
                foreach (Food food in orderStation.Restaurant.Foods)
                {
                    AverageRateFood tempFood = new AverageRateFood
                    {
                        food = await _context.Foods.Include(e => e.FoodRates).Include(e => e.FoodIngredients).FirstOrDefaultAsync(e => e.Id == food.Id)
                    };
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
                    temp.foods.Add(tempFood);
                }
                temp.orderStation.Restaurant.Foods = null;
                bodyOrderStations.Add(temp);
            }

            var result = MapOrderStationsToWsOrderStations(bodyOrderStations);

            return result;
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("DishesFromOrderStation")]
        public async Task<ActionResult<WsOrderStation>> GetDishesFromOrderStation_User([FromQuery] long OrderStationId)
        {
            OrderStation orderStation = await _context.OrderStations.Include(o => o.City).FirstOrDefaultAsync(e => e.Id == OrderStationId);
            BodyOrderStation bodyOrderStations = new BodyOrderStation
            {
                foods = new List<AverageRateFood>(),
                orderStation = orderStation
            };
            Restaurant restaurant = await _context.Restaurants.Where(f => f.Id == orderStation.RestaurantId).Include(e => e.Foods).FirstOrDefaultAsync();
            orderStation.Restaurant = restaurant;
            foreach (Food food in orderStation.Restaurant.Foods)
            {
                AverageRateFood tempFood = new AverageRateFood
                {
                    food = await _context.Foods.Include(e => e.FoodRates).Include(e => e.FoodIngredients).FirstOrDefaultAsync(e => e.Id == food.Id)
                };
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
            bodyOrderStations.orderStation.Restaurant.Foods = null;

            var result = MapOrderStationsToWsOrderStations(new List<BodyOrderStation>() { bodyOrderStations });

            return result.FirstOrDefault();
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
            var authUser = await _context.OrderStations.Include(o => o.City).FirstOrDefaultAsync(o => o.Id == stationId);
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
            var authUser = await _context.OrderStations.FirstOrDefaultAsync(o => o.Id == stationId);
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
        // In Owner Registration orderStation is registered
        [HttpPost]
        public async Task<IActionResult> PostOrderStation([Bind("address,username,password, city")] WsStation user)
        {
            return NotFound();
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


            return StatusCode(201);
        }

        // POST: api/OrderStations/auth
        // Authorization
        [HttpPost("auth")]
        public async Task<ActionResult<WsStationLoginResponse>> Login(LoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var findUser = await _context.OrderStations.Include(u => u.City)
                    .FirstOrDefaultAsync(m => m.Username == user.Username && m.Password == user.Password);
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
        [Authorize]
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
                OrderStation station = _context.OrderStations.FirstOrDefault(s => s.Id == stationId);
            }
            return stationId;
        }
        private List<WsOrderStation> MapOrderStationsToWsOrderStations(List<BodyOrderStation> bodyOrderStation)
        {
            var wsOrderStations = new List<WsOrderStation>();

            bodyOrderStation.ForEach(bos =>
            {
                var mywsDishWithRates = new List<WsDishWithRate>();
                bos.foods.ForEach(f =>
                {
                    var myDish = new WsDishWithRate()
                    {
                        Id = f.food.Id,
                        Name = f.food.Name.Trim(),
                        Price = f.food.Price,
                        Ingredients = f.food.FoodIngredients.Select(fi => fi.Ingredient.Name.Trim()).ToList(),
                        Rate = f.averageRate
                    };
                    mywsDishWithRates.Add(myDish);
                });

                var wsos = new WsOrderStation()
                {
                    Id = bos.orderStation.Id,
                    Address = bos.orderStation.Address.Trim(),
                    City = bos.orderStation.City.Name.Trim(),
                    WsRestaurant = new WsRestaurant()
                    {
                        Id = bos.orderStation.Restaurant.Id,
                        Name = bos.orderStation.Restaurant.Name.Trim(),
                        WsDishWithRates = mywsDishWithRates,
                    }
                };

                wsOrderStations.Add(wsos);
            });

            return wsOrderStations;
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using Backend.Helpers;
using Backend.RestModel;
using System.Collections.Generic;

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

        [Authorize]
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

        [Authorize]
        [HttpGet("DishesFromOrderStations")]
        public async Task<ActionResult<IEnumerable<BodyOrderStation>>> GetDishOrderStations_User()
        {
            IEnumerable<OrderStation> orderStations = await _context.OrderStations.Include(e => e.Restaurant).Include(e =>e.Restaurant.Foods).ToListAsync();
            List<BodyOrderStation> bodyOrderStations = new List<BodyOrderStation>();
            foreach(OrderStation orderStation in orderStations)
            {
                BodyOrderStation temp = new BodyOrderStation();
                temp.foods = new List<AverageRateFood>();
                temp.orderStation = orderStation;
                foreach(Food food in orderStation.Restaurant.Foods)
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
                temp.orderStation.Restaurant.Foods = null;
                bodyOrderStations.Add(temp);
            }
            return bodyOrderStations;
        }

        [Authorize]
        [HttpGet("DishesFromOrderStations/{id}")]
        public async Task<ActionResult<BodyOrderStation>> GetDishOrderStations_User([FromQuery]long OrderStationId)
        {
            OrderStation orderStation = await _context.OrderStations.Include(e => e.Restaurant).Include(e => e.Restaurant.Foods).FirstOrDefaultAsync(e => e.Id == OrderStationId);
            BodyOrderStation bodyOrderStations = new BodyOrderStation();
            bodyOrderStations.foods = new List<AverageRateFood>();
            bodyOrderStations.orderStation = orderStation;
            foreach (Food food in orderStation.Restaurant.Foods)
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
            bodyOrderStations.orderStation.Restaurant.Foods = null;
            return bodyOrderStations;
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
            }
            return stationId;
        }
    }
}

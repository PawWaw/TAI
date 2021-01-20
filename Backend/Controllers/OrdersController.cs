using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using Backend.Helpers;

namespace Backend.Controllers
{
    public class BodyUserOrder
    {
        public Order order { get; set; }
        public OrderStation orderStation { get; set; }
        public List<Food> food { get; set; }
    }

    public class UserOrder
    {
        public List<DishOrder> dishes { get; set; }
    }

    public class DishOrder
    {
        public List<FoodOrdered> foods { get; set; }
        public long orderStationId { get; set; }
    }

    public class FoodOrdered
    {
        public long dishId { get; set; }
        public int count { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DragorantContext _context;

        public class Data
        {
            public long id { get; set; }
        }

        public OrdersController(DragorantContext context)
        {
            _context = context;
        }
//Do Autoryzacji
        // GET: api/Orders
        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }
//Do Autoryzacji
        // GET: api/Orders/5 
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder_OwnerStationDeliverer(long id)
        {
            var order = await _context.Orders.Include(e => e.OrderStation).Include(e => e.Deliverer).Include(e => e.User).Include(e => e.User.City).SingleOrDefaultAsync(e => e.Id == id); ;

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
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
        [HttpPost("SubmitOrder")]
        [Authorize]
        public async Task<ActionResult<Order>> PostOrder_User(UserOrder userOrder)
        {
            long id = (long)HttpContext.Items["userId"];
            foreach (DishOrder tempDishOrder in userOrder.dishes)
            {
                Order order = new Order();
                order.DelivererId = null;
                order.EndTime = null;
                order.StartTime = DateTime.Now;
                order.Status = "STARTED";
                order.OrderStationId = tempDishOrder.orderStationId;
                order.UserId = id;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                foreach (FoodOrdered tempFoodOrdered in tempDishOrder.foods)
                {
                    List<FoodOrder> tempList = new List<FoodOrder>();
                    for (int i = 0; i < tempFoodOrdered.count; ++i)
                    {
                        FoodOrder tempFoodOrder = new FoodOrder();//Do przetestowania
                        tempFoodOrder.FoodId = tempFoodOrdered.dishId;
                        tempFoodOrder.OrderId = order.Id;
                        tempList.Add(tempFoodOrder);
                        _context.FoodOrders.Add(tempFoodOrder);
                    }
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();

            }
            return StatusCode(201);
        }
        //Do Autoryzacji
        // DELETE: api/Orders/5
        [Authorize]
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

        [Authorize]
        [HttpGet("isCurrent")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders_Deliverer([FromQuery] bool current)
        {
            long id = (long)HttpContext.Items["delivererId"];
            if (current)
            {
                return await _context.Orders.Where(e => e.Status != "ENDED" && e.DelivererId == id)
                    .Include(e => e.OrderStation).Include(e => e.Deliverer)
                    .Include(e => e.User).Include(e => e.User.City).ToListAsync();
            }
            else
            {
                return await _context.Orders.Where(e => e.Status == "ENDED" && e.DelivererId == id)
                    .Include(e => e.OrderStation).Include(e => e.Deliverer)
                    .Include(e => e.User).Include(e => e.User.City).ToListAsync();
            }
        }

        [Authorize]
        [HttpGet("UserOrders")]
        public async Task<ActionResult<IEnumerable<BodyUserOrder>>> GetUserOrders_User()
        {
            long id = (long)HttpContext.Items["userId"];
            IEnumerable<Order> orders = await _context.Orders.Where(e => e.UserId == id).Where(e => e.Status == "ENDED" || e.Status == "REALIZE").Include(e => e.OrderStation).Include(e => e.FoodOrders).Include(e => e.OrderStation.Resteurant).ToListAsync();
            List<BodyUserOrder> bodyUserOrder = new List<BodyUserOrder>();
            
            foreach(Order tempOrder in orders)
            {
                BodyUserOrder tempBody = new BodyUserOrder();
                tempBody.food = new List<Food>();
                tempBody.order = tempOrder;
                foreach(FoodOrder foodOrder in tempOrder.FoodOrders)
                {
                    FoodOrder tempFoodOrder = await _context.FoodOrders.Include(e => e.Food).FirstOrDefaultAsync(e => e.Id == tempOrder.Id);
                    tempBody.food.Add(tempFoodOrder.Food);
                }
                tempBody.orderStation = tempOrder.OrderStation;
                tempBody.orderStation.Resteurant.Foods = null;
                bodyUserOrder.Add(tempBody);
            }
            return bodyUserOrder;
        }
//Konflikt Paweł W.
/* Ordersy dla restauracji
 * Tokenem autoryzacja
 * */
        [Authorize]
        [HttpGet("/isActive")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders_OwnerStation([FromQuery] bool current)
        {
            long id = 0L;
            List<Order> orders = null;
            if(HttpContext.Items["ownerId"]!=null)
            {
                id = (long)HttpContext.Items["ownerId"];
                var owner = _context.Owners.FirstOrDefault(o => o.Id == id);
                if(owner == null)
                {
                    return NotFound();
                }
                if(current)
                {
                    orders = await _context.Orders.Where(o => o.Id == id && o.Status != "ENDED").Include(e => e.OrderStation).Include(e => e.Deliverer).Include(e => e.User).Include(e => e.User.City).ToListAsync();
                }
                else
                {
                    orders = await _context.Orders.Where(o => o.Id == id && o.Status == "ENDED")
                        .Include(e => e.OrderStation).Include(e => e.Deliverer)
                        .Include(e => e.User).Include(e => e.User.City).ToListAsync();
                }
            }
            else if(HttpContext.Items["stationId"] != null)
            {
                id = (long)HttpContext.Items["stationId"];
//TO:DO
                var station = _context.OrderStations
                    .Include(s=>s.Orders).FirstOrDefault(s => s.Id == id);
                if(station == null)
                {
                    return NotFound();
                }
                if(current)
                {
                    orders = station.Orders.Where(s => s.Status != "ENDED").ToList();
                }
            }
            else
            {
                return Unauthorized();
            }
            List<long> orderIds = await _context.Orders.Where(f => f.UserId == id).Select(a => a.Id).ToListAsync();

            if (current)
            {
                return await _context.Orders.Where(e => e.Status != "ENDED").Where(f => orderIds.Contains(f.Id)).Include(e => e.OrderStation).Include(e => e.Deliverer).Include(e => e.User).Include(e => e.User.City).ToListAsync();
            }
            else
            {
                return await _context.Orders.Where(e => e.Status == "ENDED").Where(f => orderIds.Contains(f.Id)).Include(e => e.OrderStation).Include(e => e.Deliverer).Include(e => e.User).Include(e => e.User.City).ToListAsync();
            }

        }

        //delete from query and get first with not Ended
        [Authorize]
        [HttpGet("find")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderToRealise_Deliverer()
        {
            //do dokonczenia
                return await _context.Orders.Where(e => e.Status == "STARTED").ToListAsync();

        }

        [Authorize]
        [HttpPost("take")]
        public async Task<ActionResult<Order>> RealizeOrder_Deliverer(Data data)
        {
            var order = await _context.Orders.FindAsync(data.id);
            if (order == null)
            {
                return NotFound();
            }
            order.DelivererId = (long)HttpContext.Items["delivererId"];
            order.Status = "REALIZE";
            await _context.SaveChangesAsync();

            return order;
        }

        [Authorize]
        [HttpPost("delivered")]
        public async Task<ActionResult<Order>> DeliverOrder_Deliverer(Data data)
        {
            var order = await _context.Orders.FindAsync(data.id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = "ENDED";
            order.EndTime = DateTime.Now;
            await _context.SaveChangesAsync();

            return order;
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<Order>> PatchOrderDeliveryOwnerStation(Order response)
        {
            var order = await _context.Orders.FindAsync(response.Id);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = response.Status;
            if (response.DelivererId != null)
            {
                order.DelivererId = response.DelivererId;
            }
            
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(long id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}

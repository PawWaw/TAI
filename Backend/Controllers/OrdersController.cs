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
        [HttpPost("SubmitOrder")]
        public async Task<ActionResult<Order>> PostOrder(UserOrder userOrder)
        {
            long id = 1; /////////////////////////////////////////////////////// DODAJ TOKENA TUTAJ BARTEK
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
                    FoodOrder tempFoodOrder = new FoodOrder();
                    tempFoodOrder.FoodId = tempFoodOrdered.dishId;
                    tempFoodOrder.OrderId = order.Id;
                }
                await _context.SaveChangesAsync();

            }
            return StatusCode(201);
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

        [HttpGet("UserOrders")]
        public async Task<ActionResult<IEnumerable<BodyUserOrder>>> GetUserOrders()
        {
            /////////////////////////////////////token zamiast id
            long id = 1;
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

        [HttpGet("{username}/isCurrent")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders([FromQuery] bool current, string username)
        {
            User user = await _context.Users.Where(f => f.Username == username).FirstOrDefaultAsync();
            List<long> orderIds = await _context.Orders.Where(f => f.UserId == user.Id).Select(a => a.Id).ToListAsync();

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
        [HttpGet("find")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersToRealise([FromQuery] double latitude, [FromQuery] double longitude)
        {
            //do dokonczenia
                return await _context.Orders.Where(e => e.Status != "ENDED").ToListAsync();

        }

        [HttpPost("take")]
        public async Task<ActionResult<Order>> RealizeOrder(Data data)
        {
            var order = await _context.Orders.FindAsync(data.id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = "REALIZE";
            await _context.SaveChangesAsync();

            return order;
        }

        [HttpPost("delivered")]
        public async Task<ActionResult<Order>> DeliverOrder(Data data)
        {
            var order = await _context.Orders.FindAsync(data.id);
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
        public async Task<ActionResult<Order>> PatchOrderDelivery(Order response)
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

        //[HttpPatch("{id}")]
        //public async Task<ActionResult<Order>> PatchOrder(long id)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Orders.Remove(order);
        //    await _context.SaveChangesAsync();

        //    return order;
        //}
        private bool OrderExists(long id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}

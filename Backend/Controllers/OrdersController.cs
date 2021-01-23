﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using Backend.Helpers;
using Backend.RestModel;
using Backend.RestModel.ForDeliverer;

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
        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5 
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<WsExtendedDateWsOrderResponse>> GetOrder_Deliverer(long id)
        {
            var order = await _context.Orders.Include(e => e.OrderStation.City).Include(e => e.Deliverer).Include(e => e.User).Include(e => e.User.City).SingleOrDefaultAsync(e => e.Id == id); ;

            if (order == null)
            {
                return NotFound();
            }

            return Ok(new WsExtendedDateWsOrderResponse {
                Id = order.Id,
                Status = order.Status,
                StartDate = order.StartTime.ToString("s"),
                EndDate = order.EndTime?.ToString("s"),
                ClientAddress = new WsAddress
                {
                    Address = order.User.Address,
                    City = order.User.City.Name
                },
                RestaurantAddress = new WsAddress
                {
                    Address = order.OrderStation.Address,
                    City = order.OrderStation.City.Name
                }
            });
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
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            foreach (DishOrder tempDishOrder in userOrder.dishes)
            {
                Order order = new Order
                {
                    DelivererId = null,
                    EndTime = null,
                    StartTime = DateTime.Now,
                    Status = "STARTED",
                    OrderStationId = tempDishOrder.orderStationId,
                    UserId = id
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                foreach (FoodOrdered tempFoodOrdered in tempDishOrder.foods)
                {
                    List<FoodOrder> tempList = new List<FoodOrder>();
                    for (int i = 0; i < tempFoodOrdered.count; ++i)
                    {
                        FoodOrder tempFoodOrder = new FoodOrder
                        {
                            FoodId = tempFoodOrdered.dishId,
                            OrderId = order.Id
                        };
                        tempList.Add(tempFoodOrder);
                        _context.FoodOrders.Add(tempFoodOrder);
                    }
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();

            }
            return StatusCode(201);
        }
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
        public async Task<ActionResult<IEnumerable<WsEndDateWsOrderResponse>>> GetOrders_Deliverer([FromQuery] bool current)
        {
            long id = (long)HttpContext.Items["delivererId"];
            Deliverer deliverer = await _context.Deliverers.FirstOrDefaultAsync(d => d.Id == id);
            if(deliverer==null)
            {
                return NotFound();
            }
            List<Order> orders;
            if (current)
            {
                 orders = await _context.Orders.Where(e => e.Status.Trim() != "ENDED" && e.DelivererId == id)
                    .Include(e => e.OrderStation).Include(e => e.Deliverer)
                    .Include(e => e.User).Include(e => e.User.City).ToListAsync();
            }
            else
            {
                orders = await _context.Orders.Where(e => e.Status.Trim() == "ENDED" && e.DelivererId == id)
                    .Include(e => e.OrderStation).Include(e => e.Deliverer)
                    .Include(e => e.User).Include(e => e.User.City).ToListAsync();
            }
            List<WsEndDateWsOrderResponse> result = new List<WsEndDateWsOrderResponse>();
            orders.ForEach(o =>
                result.Add(new WsEndDateWsOrderResponse
                {
                    Id = o.Id,
                    ClientAddress = new WsAddress
                    {
                        Address = o.User.Address,
                        City = o.User.City.Name
                    },
                    RestaurantAddress = new WsAddress
                    {
                        Address = o.OrderStation.Address,
                        City = o.OrderStation.City.Name
                    },
                    EndDate = o.EndTime?.ToString("s")
                })
            );
            return result;
        }

        [Authorize]
        [HttpGet("UserOrders")]
        public async Task<ActionResult<IEnumerable<BodyUserOrder>>> GetUserOrders_User()
        {
            long id = (long)HttpContext.Items["userId"];
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            IEnumerable<Order> orders = await _context.Orders.Where(e => e.UserId == id).Where(e => e.Status.Trim() == "ENDED" || e.Status.Trim() == "REALIZE").Include(e => e.OrderStation).Include(e => e.FoodOrders).Include(e => e.OrderStation.Restaurant).ToListAsync();
            List<BodyUserOrder> bodyUserOrder = new List<BodyUserOrder>();
            
            foreach(Order tempOrder in orders)
            {
                BodyUserOrder tempBody = new BodyUserOrder
                {
                    food = new List<Food>(),
                    order = tempOrder
                };
                foreach (FoodOrder foodOrder in tempOrder.FoodOrders)
                {
                    FoodOrder tempFoodOrder = await _context.FoodOrders.Include(e => e.Food).FirstOrDefaultAsync(e => e.Id == tempOrder.Id);
                    tempBody.food.Add(tempFoodOrder.Food);
                }
                tempBody.orderStation = tempOrder.OrderStation;
                tempBody.orderStation.Restaurant.Foods = null;
                bodyUserOrder.Add(tempBody);
            }
            return bodyUserOrder;
        }

        [Authorize]
        [HttpGet("isActive")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders_Owner([FromQuery] bool current)
        {
            long id = 0L;
            List<Order> orders = null;
            if (HttpContext.Items["ownerId"] != null)
            {
                id = (long)HttpContext.Items["ownerId"];
                var owner = _context.Owners.Include(o=>o.Restaurant.OrderStation.Orders).FirstOrDefault(o => o.Id == id);
                if (owner == null)
                {
                    return NotFound();
                }
                if (current)
                {
                    orders = owner.Restaurant.OrderStation.Orders.Where(s => s.Status.Trim() != "ENDED").ToList();
                    foreach(Order order in orders)
                    {
                        Deliverer deliverer = await _context.Deliverers.FirstOrDefaultAsync(d => d.Id == order.DelivererId);
                    }
                }
                else
                {
                    orders = owner.Restaurant.OrderStation.Orders.Where(s => s.Status.Trim() == "ENDED").ToList();
                    foreach (Order order in orders)
                    {
                        Deliverer deliverer = await _context.Deliverers.FirstOrDefaultAsync(d => d.Id == order.DelivererId);
                    }
                }
            }
            else if (HttpContext.Items["stationId"] != null)
            {
                id = (long)HttpContext.Items["stationId"];
                var station = _context.OrderStations
                    .Include(s => s.Orders).FirstOrDefault(s => s.Id == id);
                if (station == null)
                {
                    return NotFound();
                }
                if (current)
                {
                    orders = station.Orders.Where(s => s.Status.Trim() != "ENDED").ToList();
                    foreach (Order order in orders)
                    {
                        Deliverer deliverer = await _context.Deliverers.FirstOrDefaultAsync(d => d.Id == order.DelivererId);
                    }
                }
                else
                {
                    orders = station.Orders.Where(s => s.Status.Trim() == "ENDED").ToList();
                    foreach (Order order in orders)
                    {
                        Deliverer deliverer = await _context.Deliverers.FirstOrDefaultAsync(d => d.Id == order.DelivererId);
                    }
                }
            }
            else
            {
                return Unauthorized();
            }
            return Ok(orders);

        }

        [Authorize]
        [HttpGet("find")]
        public async Task<ActionResult<WsOrderResponse>> GetOrderToRealise_Deliverer()
        {
            var order = await _context.Orders.Include(o => o.OrderStation.City)
                                            .Include(o => o.User.City).Where(e => e.Status.Trim() == "STARTED")
                                            .OrderBy(e => e.StartTime.Date.Day).FirstOrDefaultAsync();
            if(order == null)
            {
                return NotFound();
            }
            WsOrderResponse response = new WsOrderResponse
            {
                ClientAddress = new WsAddress { Address = order.User.Address, City = order.User.City.Name },
                RestaurantAddress = new WsAddress { Address = order.OrderStation.Address, City = order.OrderStation.City.Name },
                Id = order.Id
            };
            return Ok(response);
        }

        [Authorize]
        [HttpPost("take")]
        public async Task<ActionResult<Order>> RealizeOrder_Deliverer(Data data)
        {
            long delivererId = (long)HttpContext.Items["delivererId"];
            Deliverer deliverer = await _context.Deliverers.FirstOrDefaultAsync(d => d.Id == delivererId);
            if(deliverer == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(data.id);
            if (order == null)
            {
                return NotFound();
            }
            order.DelivererId = delivererId;
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
        public async Task<ActionResult<Order>> PatchOrderDelivery_OwnerStation(Order response)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestarauntWebApplication.Hubs;
using RestarauntWebApplication.Models;
using RestarauntWebApplication.Models.EFModels;

namespace RestarauntWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IHubContext<NotificationHub> _hubContext;
        private readonly RestarauntContext _context;

        public OrdersController(RestarauntContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }


        [HttpPut("UpdateOrderStatus/{orderId}")]
        public async Task<ActionResult> UpdateOrderStatus(int orderId) 
        {
            Order updateOrder = _context.Orders.Where(p => p.OrderId.Equals(orderId)).FirstOrDefault();
            updateOrder.OrderStatus = "Неактивен";
            _context.Entry(updateOrder).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }
        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
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

        [HttpGet("OrdersOfWaiter/{workerLogin}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersOfWaiter(string workerLogin)
        {
            var worker = _context.Workers.FirstOrDefault(p => p.WorkerLogin.Equals(workerLogin));
            var waiter = _context.Waiters.FirstOrDefault(p => p.WorkerId.Equals(worker.WorkerId));
            var orders = await _context.Orders.Where(p => p.WaiterId.Equals(waiter.WaiterId)).ToListAsync();
            return orders;
        }


        [HttpPost("CompositeOrder")]
        public async Task<ActionResult> PostOrder(CompositeOrderModel compositeOrder)
        {
            var _worker = _context.Workers.FirstOrDefault(p => p.WorkerLogin.Equals(compositeOrder.WaiterLogin));
            var _waiter = _context.Waiters.FirstOrDefault(p => p.WorkerId.Equals(_worker.WorkerId));
            Order order = new Order {VisitorId = null, WaiterId = _waiter.WaiterId, OrderNumber = compositeOrder.OrderNumber, OrderStatus = compositeOrder.OrderStatus};

            foreach (var item in compositeOrder.DishCookOrders)
            {
                order.DishCookOrders.Add(item);
            }
            _context.Orders.Add(order);
            _context.SaveChanges();

            await _hubContext.Clients.All.SendAsync("UpdateOrders", JsonConvert.SerializeObject(_context.DishCookOrders.Where(p => p.CookId == null).Include(p => p.Dish).ToList(), Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

            return Ok(order.OrderId);
        }


        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}

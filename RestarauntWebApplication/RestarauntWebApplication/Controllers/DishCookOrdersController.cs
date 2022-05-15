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
    public class DishCookOrdersController : ControllerBase
    {
        IHubContext<NotificationHub> _hubContext;
        private readonly RestarauntContext _context;

        public DishCookOrdersController(RestarauntContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/DishCookOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishCookOrder>>> GetDishCookOrders()
        {
            return await _context.DishCookOrders.Where(p => p.CookId == null).Where(p => p.DishStatus.Equals("Не готово")).Include(p => p.Dish).ToListAsync();
        }

        [HttpGet("DishDetails/{orderId}")]
        public async Task<ActionResult<IEnumerable<DishCookOrder>>> GetDishDetails(int orderId)
        {
            return await _context.DishCookOrders.Where(p => p.OrderId.Equals(orderId)).Include(p => p.Dish).ToListAsync();
        }

        [HttpGet("GetDishToCooking/{cookLogin}")]
        public async Task<ActionResult<IEnumerable<DishCookOrder>>> GetDishToCooking(string cookLogin)
        {
            var worker = _context.Workers.FirstOrDefault(p => p.WorkerLogin.Equals(cookLogin));
            var cook = _context.Cooks.FirstOrDefault(p => p.WorkerId.Equals(worker.WorkerId));
            return await _context.DishCookOrders.Where(p => p.CookId.Equals(cook.CookId)).Where(p => p.DishStatus.Equals("Готовится")).Include(p=>p.Dish).ToListAsync();
        }

        // GET: api/DishCookOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DishCookOrder>> GetDishCookOrder(int id)
        {
            var dishCookOrder = await _context.DishCookOrders.FindAsync(id);

            if (dishCookOrder == null)
            {
                return NotFound();
            }

            return dishCookOrder;
        }

        [HttpPut("UpdateCookIdDCO")]
        public async Task<ActionResult> UpdateCookIdDCO(DishCookOrderUpdateModel dishCookOrderUpdateModel)
        {
            var worker = _context.Workers.Where(p => p.WorkerLogin.Equals(dishCookOrderUpdateModel.Login)).FirstOrDefault();
            var cook = _context.Cooks.Where(p => p.WorkerId.Equals(worker.WorkerId)).FirstOrDefault();

            DishCookOrder dishCookOrderUpdateItem = _context.DishCookOrders.Include(p=>p.Dish).Where(p => p.OrderId.Equals(dishCookOrderUpdateModel.DishCookOrder.OrderId)).Where(p => p.DishId.Equals(dishCookOrderUpdateModel.DishCookOrder.DishId)).ToList().FirstOrDefault();
            dishCookOrderUpdateItem.CookId = cook.CookId;
            dishCookOrderUpdateItem.DishStatus = "Готовится"; 
            _context.Entry(dishCookOrderUpdateItem).State = EntityState.Modified;
            _context.SaveChanges();
            
            await _hubContext.Clients.All.SendAsync("UpdateStatusOrders", JsonConvert.SerializeObject(dishCookOrderUpdateItem, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Ok();
        }

        [HttpPut("UpdateDishStatusDCO")]
        public async Task<ActionResult> UpdateDishStatusDCO(DishCookOrder dishCookOrder)
        {
            DishCookOrder dishCookOrderUpdateItem = _context.DishCookOrders.Include(p => p.Dish).FirstOrDefault(p => p.OrderId.Equals(dishCookOrder.OrderId) && p.DishId.Equals(dishCookOrder.DishId));
            dishCookOrderUpdateItem.DishStatus = "Готово";
            _context.Entry(dishCookOrderUpdateItem).State = EntityState.Modified;
            _context.SaveChanges();
            
            await _hubContext.Clients.All.SendAsync("UpdateStatusOrders", JsonConvert.SerializeObject(dishCookOrderUpdateItem, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Ok();
        }

        [HttpPut("UpdateDishGiven")]
        public async Task<ActionResult> UpdateDishGiven(DishCookOrder dishCookOrder)
        {
            DishCookOrder dishCookOrderUpdateItem = _context.DishCookOrders.Include(p => p.Dish).FirstOrDefault(p => p.OrderId.Equals(dishCookOrder.OrderId) && p.DishId.Equals(dishCookOrder.DishId));
            dishCookOrderUpdateItem.DishStatus = "Доставлено";
            _context.Entry(dishCookOrderUpdateItem).State = EntityState.Modified;
            _context.SaveChanges();
            await _hubContext.Clients.All.SendAsync("UpdateStatusOrders", JsonConvert.SerializeObject(dishCookOrderUpdateItem, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Ok();
        }


        // PUT: api/DishCookOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDishCookOrder(int id, DishCookOrder dishCookOrder)
        {
            if (id != dishCookOrder.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(dishCookOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishCookOrderExists(id))
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

        // POST: api/DishCookOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DishCookOrder>> PostDishCookOrder(DishCookOrder dishCookOrder)
        {
            _context.DishCookOrders.Add(dishCookOrder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DishCookOrderExists(dishCookOrder.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDishCookOrder", new { id = dishCookOrder.OrderId }, dishCookOrder);
        }

        // DELETE: api/DishCookOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDishCookOrder(int id)
        {
            var dishCookOrder = await _context.DishCookOrders.FindAsync(id);
            if (dishCookOrder == null)
            {
                return NotFound();
            }

            _context.DishCookOrders.Remove(dishCookOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DishCookOrderExists(int id)
        {
            return _context.DishCookOrders.Any(e => e.OrderId == id);
        }
    }
}

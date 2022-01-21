//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using RestarauntWebApplication.Hubs;
//using RestarauntWebApplication.Models.EFModels;

//namespace RestarauntWebApplication.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class KitchensController : ControllerBase
//    {
//        IHubContext<NotificationHub> _hubContext;
//        private readonly RestarauntContext _context;

//        public KitchensController(RestarauntContext context, IHubContext<NotificationHub> hubContext)
//        {
//            _context = context;
//            _hubContext = hubContext;
//        }

//        // GET: api/Kitchens
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Kitchen>>> GetKitchens()
//        {
//            return await _context.Kitchens.ToListAsync();

//        }

//        [HttpGet("DishesForCooking")]
//        public async Task<ActionResult<IEnumerable<Kitchen>>> GetDishesForCooking()
//        {


//            return await _context.Kitchens.Where(p => p.DishStatus.Equals("Не готово")).ToListAsync();
//        }

//        // GET: api/Kitchens/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Kitchen>> GetKitchen(int id)
//        {
//            var kitchen = await _context.Kitchens.FindAsync(id);

//            if (kitchen == null)
//            {
//                return NotFound();
//            }

//            return kitchen;
//        }

//        // PUT: api/Kitchens/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutKitchen(int id, Kitchen kitchen)
//        {
//            if (id != kitchen.OrderId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(kitchen).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!KitchenExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        [HttpPost("ListKitchens")]
//        public async Task<ActionResult> PostKitchen(List<Kitchen> kitchens)
//        {
//            foreach (var item in kitchens)
//            {
//                _context.Kitchens.Add(item);
//            }
//            _context.SaveChanges();

//            //await _hubContext.Clients.All.SendAsync("UpdateKitchens", _context.Kitchens.Include(p=>p.).ToList());
//            return Ok();
//        }
//        // POST: api/Kitchens
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Kitchen>> PostKitchen(Kitchen kitchen)
//        {

//            _context.Kitchens.Add(kitchen);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (KitchenExists(kitchen.OrderId))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetKitchen", new { id = kitchen.OrderId }, kitchen);
//        }

//        // DELETE: api/Kitchens/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteKitchen(int id)
//        {
//            var kitchen = await _context.Kitchens.FindAsync(id);
//            if (kitchen == null)
//            {
//                return NotFound();
//            }

//            _context.Kitchens.Remove(kitchen);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool KitchenExists(int id)
//        {
//            return _context.Kitchens.Any(e => e.OrderId == id);
//        }
//    }
//}

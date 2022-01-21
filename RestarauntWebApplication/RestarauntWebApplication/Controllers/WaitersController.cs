using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestarauntWebApplication.Models.EFModels;

namespace RestarauntWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaitersController : ControllerBase
    {
        private readonly RestarauntContext _context;

        public WaitersController(RestarauntContext context)
        {
            _context = context;
        }

        // GET: api/Waiters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Waiter>>> GetWaiters()
        {
            return await _context.Waiters.ToListAsync();
        }

        [HttpGet("WaiterLogin/{waiterLogin}")]
        public async Task<ActionResult<IEnumerable<Waiter>>> GetOrdersOfWaiter(string waiterLogin)
        {
            var worker = _context.Workers.FirstOrDefault(p => p.WorkerLogin.Equals(waiterLogin));
            return await _context.Waiters.Where(p => p.WorkerId.Equals(worker.WorkerId)).Include(p => p.Orders).ToListAsync();
        }

        // GET: api/Waiters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Waiter>> GetWaiter(int id)
        {
            var waiter = await _context.Waiters.FindAsync(id);

            if (waiter == null)
            {
                return NotFound();
            }

            return waiter;
        }

        // PUT: api/Waiters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWaiter(int id, Waiter waiter)
        {
            if (id != waiter.WaiterId)
            {
                return BadRequest();
            }

            _context.Entry(waiter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaiterExists(id))
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

        // POST: api/Waiters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Waiter>> PostWaiter(Waiter waiter)
        {
            _context.Waiters.Add(waiter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWaiter", new { id = waiter.WaiterId }, waiter);
        }

        // DELETE: api/Waiters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaiter(int id)
        {
            var waiter = await _context.Waiters.FindAsync(id);
            if (waiter == null)
            {
                return NotFound();
            }

            _context.Waiters.Remove(waiter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WaiterExists(int id)
        {
            return _context.Waiters.Any(e => e.WaiterId == id);
        }
    }
}

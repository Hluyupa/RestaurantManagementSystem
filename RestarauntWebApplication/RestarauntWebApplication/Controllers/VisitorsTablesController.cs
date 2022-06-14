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
using RestarauntWebApplication.Models.EFModels;

namespace RestarauntWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsTablesController : ControllerBase
    {
        private readonly RestarauntContext _context;
        IHubContext<NotificationHub> _hubContext;
        public VisitorsTablesController(RestarauntContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/VisitorsTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitorsTable>>> GetVisitorsTables()
        {
            return await _context.VisitorsTables.ToListAsync();
        }

        [HttpGet("GetBookingList")]
        public async Task<ActionResult<IEnumerable<VisitorsTable>>> GetBookingList()
        {
            return await _context.VisitorsTables.Include(p=>p.Table).Include(p=>p.Visitor).ToListAsync();
        }



        // GET: api/VisitorsTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitorsTable>> GetVisitorsTable(int id)
        {
            var visitorsTable = await _context.VisitorsTables.FindAsync(id);

            if (visitorsTable == null)
            {
                return NotFound();
            }

            return visitorsTable;
        }

        // PUT: api/VisitorsTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitorsTable(int id, VisitorsTable visitorsTable)
        {
            if (id != visitorsTable.VisitorId)
            {
                return BadRequest();
            }

            _context.Entry(visitorsTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorsTableExists(id))
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

        // POST: api/VisitorsTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VisitorsTable>> PostVisitorsTable(VisitorsTable visitorsTable)
        {
            _context.VisitorsTables.Add(visitorsTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisitorsTable", new { id = visitorsTable.BookingId }, visitorsTable);
        }

        // DELETE: api/VisitorsTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitorsTable(int id)
        {
            var visitorsTable = await _context.VisitorsTables.Include(p=>p.Table).Include(p=>p.Visitor).FirstOrDefaultAsync(p=>p.BookingId==id);
            if (visitorsTable == null)
            {
                return NotFound();
            }

            _context.VisitorsTables.Remove(visitorsTable);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("DeletedBooking", JsonConvert.SerializeObject(visitorsTable, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Ok();
        }

        private bool VisitorsTableExists(int id)
        {
            return _context.VisitorsTables.Any(e => e.VisitorId == id);
        }
    }
}

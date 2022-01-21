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
    public class DishTypesController : ControllerBase
    {
        private readonly RestarauntContext _context;

        public DishTypesController(RestarauntContext context)
        {
            _context = context;
        }

        // GET: api/DishTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishType>>> GetDishTypes()
        {
            return await _context.DishTypes.ToListAsync();
        }

        // GET: api/DishTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DishType>> GetDishType(int id)
        {
            var dishType = await _context.DishTypes.FindAsync(id);

            if (dishType == null)
            {
                return NotFound();
            }

            return dishType;
        }

        // PUT: api/DishTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDishType(int id, DishType dishType)
        {
            if (id != dishType.DishTypeId)
            {
                return BadRequest();
            }

            _context.Entry(dishType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishTypeExists(id))
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

        // POST: api/DishTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DishType>> PostDishType(DishType dishType)
        {
            _context.DishTypes.Add(dishType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDishType", new { id = dishType.DishTypeId }, dishType);
        }

        // DELETE: api/DishTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDishType(int id)
        {
            var dishType = await _context.DishTypes.FindAsync(id);
            if (dishType == null)
            {
                return NotFound();
            }

            _context.DishTypes.Remove(dishType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DishTypeExists(int id)
        {
            return _context.DishTypes.Any(e => e.DishTypeId == id);
        }
    }
}

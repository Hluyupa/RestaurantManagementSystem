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
    public class DishesIngridientsController : ControllerBase
    {
        private readonly RestarauntContext _context;

        public DishesIngridientsController(RestarauntContext context)
        {
            _context = context;
        }

        // GET: api/DishesIngridients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishesIngridient>>> GetDishesIngridients()
        {
            return await _context.DishesIngridients.ToListAsync();
        }

        [HttpGet("GetDishIngridients/{dishId}")]
        public async Task<ActionResult<IEnumerable<DishesIngridient>>> GetDishIngridients(int dishId)
        {
            return await _context.DishesIngridients.Where(p => p.DishId.Equals(dishId)).Include(p => p.Ingridient).ToListAsync();
        }

        // GET: api/DishesIngridients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DishesIngridient>> GetDishesIngridient(int id)
        {
            var dishesIngridient = await _context.DishesIngridients.FindAsync(id);

            if (dishesIngridient == null)
            {
                return NotFound();
            }

            return dishesIngridient;
        }

        

        // PUT: api/DishesIngridients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDishesIngridient(int id, DishesIngridient dishesIngridient)
        {
            if (id != dishesIngridient.DishId)
            {
                return BadRequest();
            }

            _context.Entry(dishesIngridient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishesIngridientExists(id))
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

        // POST: api/DishesIngridients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DishesIngridient>> PostDishesIngridient(DishesIngridient dishesIngridient)
        {
            _context.DishesIngridients.Add(dishesIngridient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DishesIngridientExists(dishesIngridient.DishId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDishesIngridient", new { id = dishesIngridient.DishId }, dishesIngridient);
        }

        // DELETE: api/DishesIngridients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDishesIngridient(int id)
        {
            var dishesIngridient = await _context.DishesIngridients.FindAsync(id);
            if (dishesIngridient == null)
            {
                return NotFound();
            }

            _context.DishesIngridients.Remove(dishesIngridient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DishesIngridientExists(int id)
        {
            return _context.DishesIngridients.Any(e => e.DishId == id);
        }
    }
}

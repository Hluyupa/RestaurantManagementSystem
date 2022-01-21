using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RestarauntWebApplication.Hubs;
using RestarauntWebApplication.Models.EFModels;

namespace RestarauntWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        IHubContext<NotificationHub> _hubContext;
        private readonly RestarauntContext _context;

        public DishesController(RestarauntContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/Dishes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dish>>> GetDishes()
        {
            return await _context.Dishes.Include(p=>p.DishType).ToListAsync();
        }

        // GET: api/Dishes
        [HttpGet("DishesInMenu")]
        public async Task<ActionResult<IEnumerable<Dish>>> GetDishesInMenu()
        {
            return await _context.Dishes.Include(p => p.DishType).Include(p=>p.DishesIngridients).ToListAsync();
        }

        // GET: api/Dishes/DishesOfTypes/id
        /*[Route("api/[controller]/DishesOfTypes")]*/
        [HttpGet("DishesOfType/{idDishType}")]
        public async Task<ActionResult<IEnumerable<Dish>>> GetDishesOfType(int idDishType)
        {
            return await _context.Dishes.Where(p => p.DishTypeId.Equals(idDishType)).ToListAsync();
        }



        // GET: api/Dishes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dish>> GetDish(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            return dish;
        }

        [HttpPut("UpdateDishInMenu")]
        public async Task<ActionResult> UpdateDishInMenu(Dish dish)
        {
            Dish updateDish = _context.Dishes.Include(p=>p.DishesIngridients).FirstOrDefault(p => p.DishId.Equals(dish.DishId));
            updateDish.DishName = dish.DishName;
            updateDish.DishType = dish.DishType;
            updateDish.DishCost = dish.DishCost;
            updateDish.DishSeason = dish.DishSeason;
            updateDish.DishesIngridients = dish.DishesIngridients;
            _context.Entry(updateDish).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT: api/Dishes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDish(int id, Dish dish)
        {
            if (id != dish.DishId)
            {
                return BadRequest();
            }

            _context.Entry(dish).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
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

        // POST: api/Dishes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dish>> PostDish(Dish dish)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDish", new { id = dish.DishId }, dish);
        }

       


        // DELETE: api/Dishes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            var dishesIngridients = _context.DishesIngridients.Where(p => p.DishId.Equals(id));
            foreach (var item in dishesIngridients)
            {
                _context.DishesIngridients.Remove(item);
            }
            await _context.SaveChangesAsync();

            var dish = _context.Dishes.FirstOrDefault(p=>p.DishId.Equals(id));
            if (dish == null)
            {
                return NotFound();
            }

            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.DishId == id);
        }
    }
}

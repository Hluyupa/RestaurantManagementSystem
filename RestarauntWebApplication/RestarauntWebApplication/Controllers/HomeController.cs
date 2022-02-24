using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestarauntWebApplication.Models.EFModels;
using RestarauntWebApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestarauntWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestarauntContext _context;

        public HomeController(RestarauntContext context)
        {
            _context = context;
        }

        public IActionResult MainPage()
        {
            return View();
        }

        public IActionResult RestarauntMap()
        {
            return View();
        }

        public IActionResult RestarauntMenu()
        {
            var menu = _context.Dishes.Include(p => p.DishType).Include(p=>p.DishesIngridients).ToList();
            /*var menu = _context.Dishes.Select(e => new DishView() { Name = e.DishName, Cost = (decimal)(e.DishCost ?? 0), Type = e.DishType.DishTypeName }).ToList();*/
            ViewData["asas"] = new List<object>();
            return View(menu);
        }
    }
}

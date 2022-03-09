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
        private VisitorsTable newBooking;
        public HomeController(RestarauntContext context)
        {
            newBooking = new VisitorsTable();
            _context = context;
        }

        public IActionResult MainPage()
        {
            return View();
        }

        public IActionResult RestarauntMap()
        {
            var tables = _context.Tables.Include(p => p.VisitorsTables).ToList();
            return View(tables);
        }

        public IActionResult RestarauntMenu()
        {
            var menu = _context.Dishes.Include(p => p.DishType).Include(p=>p.DishesIngridients).ToList();
            /*var menu = _context.Dishes.Select(e => new DishView() { Name = e.DishName, Cost = (decimal)(e.DishCost ?? 0), Type = e.DishType.DishTypeName }).ToList();*/
            //ViewData["asas"] = new List<object>();
            return View(menu);
        }

        
        public IActionResult CreateBooking(VisitorsTable visitortable)
        {
           
            newBooking.TableId = visitortable.TableId;
            newBooking.DateBooking = visitortable.DateBooking;
            /*newBooking.VisitorId = 2;
            _context.VisitorsTables.Add(newBooking);
            _context.SaveChanges();*/
            return RedirectToAction("RestarauntUserInformationForm");
        }

        public IActionResult RestarauntUserInformationForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateVisitor(Visitor visitor)
        {
            
            newBooking.Visitor = new Visitor 
            {
                VisitorFullName = visitor.VisitorFullName, 
                VisitorEmail = visitor.VisitorEmail,
                VisitorTelephone=visitor.VisitorTelephone
            };
            //_context.Visitors.Add(newBooking.Visitor);
            //newBooking.VisitorId = 16;
            _context.VisitorsTables.Add(newBooking);
            _context.SaveChanges();
            return RedirectToAction("RestarauntMap");
        }


    }
}

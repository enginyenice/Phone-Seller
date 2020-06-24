using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelefonSatis.Data;
using TelefonSatis.Models;

namespace TelefonSatis.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataBaseContex _context;
        public HomeController(DataBaseContex context)
        {
            _context = context;
            
        }

        
        public async Task<IActionResult> Index()
        {

            ICollection<Brand> personlist = _context.Brands.ToList();
            ViewBag.data = personlist;
            var dataBaseContex = _context.Phones.Include(p => p.brand);
            /*
            ViewBag.sessionMail           = HttpContext.Session.GetString("Mail");
            ViewBag.sessionUserName       =  HttpContext.Session.GetString("userName");
            ViewBag.sessionPermission     = HttpContext.Session.GetString("Permission");
            */
            return View(await dataBaseContex.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<IActionResult> Products()
        {
            ICollection<Brand> personlist = _context.Brands.ToList();
            ViewBag.data = personlist;
            var dataBaseContex = _context.Phones.Include(p => p.brand);

            return View(await dataBaseContex.ToListAsync());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> Detail(int? id)
        {

          

            ICollection<Brand> personlist = _context.Brands.ToList();
            ViewBag.data = personlist;

            if (id == null)
            {
                return NotFound();
            }

            var phone = _context.Phones
                            .Include(m => m.brand)
                            .Include(m => m.comments)
                            .First(m => m.PhoneId == id);
                            
            if (phone == null)
            {
                return NotFound();
            }

return View(phone);
}
public IActionResult Privacy()
{
return View();
}

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
}
}

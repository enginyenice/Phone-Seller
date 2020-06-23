using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TelefonSatis.Data;
using TelefonSatis.Models;

namespace TelefonSatis.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataBaseContex _context;

        public AccountController(DataBaseContex context)
        {
            _context = context;
        }


        // GET: Users/Create
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Mail,string Password)
        {
            Console.WriteLine(Mail, Password);


            return Login();
        }



    }
}

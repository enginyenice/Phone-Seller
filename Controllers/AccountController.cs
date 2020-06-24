/*
 * TODO:Login işlemi hatalıysa geri dönüşte hata verdir..
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using TelefonSatis.Data;
using TelefonSatis.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

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
            int result = 0;
            result = _context.Users.Where(u => u.Mail == Mail && u.Password == Password).Count();
            

            if(result > 0)
            {
                //Kullanici Bilgilerini Cek
                string mail         = Mail;
                string userName     = "";
                string password     = "";
                string permission   = "";

                //HttpContext.Session.SetString("Mail", mail);
                //HttpContext.Session.SetString("userName", userName);
                //HttpContext.Session.SetString("Permission", permission);
                return View("Index");
            }else
            {

                return View("Login");
            }

            
            return View("Login");
        }



    }
}

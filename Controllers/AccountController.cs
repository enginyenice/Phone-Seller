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
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

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
            TempData["Uyari"] = "";

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
                var userProfile = _context.Users.Select(User => User).Where(u => u.Mail == Mail && u.Password == Password);
                //Kullanici Bilgilerini Cek

                string mail = "";
                string userName = "";
                string permission = "";
                foreach (var item in userProfile)
                {
                    mail        = item.Mail;
                    userName    = item.UserName;
                    permission = item.Permission;
                }




                HttpContext.Session.SetString("userName", userName);
                HttpContext.Session.SetString("Mail", mail);
                HttpContext.Session.SetString("Permission", permission);

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                TempData["Uyari"] = "Email veya parola yanlış";
                return View("Login");
            }
        }


        public IActionResult Logout()
        {
            TempData["Uyari"] = "";
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("Mail");
            HttpContext.Session.Remove("Permission");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }



    }
}

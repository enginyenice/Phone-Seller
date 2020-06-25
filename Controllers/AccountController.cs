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

        #region Login

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
                string UserId   = ""; 
                foreach (var item in userProfile)
                {
                    UserId      = item.UserId.ToString();
                    mail        = item.Mail;
                    userName    = item.UserName;
                    permission  = item.Permission;
                }



                HttpContext.Session.SetString("userId", UserId);
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
        #endregion
        #region Logout
        public IActionResult Logout()
        {
            TempData["Uyari"] = "";
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("Mail");
            HttpContext.Session.Remove("Permission");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion
        #region register
        
        // GET: Users/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string Mail,string Password,string UserName)
        {
            if (ModelState.IsValid)
            {
                int userCount =_context.Users.Where(u => u.UserName == UserName).Count();
                int mailCount = _context.Users.Where(u => u.Mail == Mail).Count();
                if(userCount > 0)
                {
                    TempData["Uyari"] = "Username Dedected...";
                    return View();

                } else if(mailCount > 0)
                {
                    TempData["Uyari"] = "Mail Dedected...";
                    return View();
                } else
                {
                    User user = new User
                    {
                        Mail = Mail,
                        Password = Password,
                        UserName = UserName,
                        Permission = "Customer"
                    };
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login");
                }
               
            }
            return View();
        }
        #endregion
        #region profile
        public async Task<IActionResult> Profile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMail(string Mail, int userId)
        {
            if (ModelState.IsValid)
            {
                int mailCount = _context.Users.Where(u => u.Mail == Mail).Count();
                if (mailCount > 0)
                {
                    TempData["Uyari"] = "Mail Dedected...";
                    return RedirectToAction("Profile");
                }
                else
                {

                    User userMailEdit = _context.Users.SingleOrDefault(k => k.UserId == userId);

                    userMailEdit.Mail = Mail;
                    _context.SaveChanges();
                    HttpContext.Session.SetString("Mail", Mail);
                    return RedirectToAction("Profile");
                }

            }
            return RedirectToAction("Profile");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(string Password, int userId)
        {
            if (ModelState.IsValid)
            {


                    User userPasswordEdit = _context.Users.SingleOrDefault(k => k.UserId == userId);

                    userPasswordEdit.Password = Password;
                    _context.SaveChanges();
                }
            return RedirectToAction("Profile");
        }


        #endregion
    }
}

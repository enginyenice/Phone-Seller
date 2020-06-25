using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelefonSatis.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace TelefonSatis.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly DataBaseContex _context;

        public  AdminPanelController(DataBaseContex context)
        {
            _context = context;
        }

        public bool SessionCont()
        {
            string permission = "";

            if (HttpContext != null)
            {
                if ((HttpContext.Session.GetString("Permission")) != null)
                {
                    permission = HttpContext.Session.GetString("Permission");
                    if (permission == "Admin")
                    {
                        return true;
                    }
                }
            }
            return false; ;
        }

        public  ActionResult UserManagment()
        {
            bool session = SessionCont();
            if(session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_context.Users.ToList());
        }
        public IActionResult PhoneManagment()
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_context.Phones.Include(p => p.brand));
        }
        public IActionResult BrandManagment()
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_context.Brands.ToList());
        }
        public IActionResult CommentManagment()
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_context.Comments.ToList());
        }
   
    }
}

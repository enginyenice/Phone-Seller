using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelefonSatis.Data;

namespace TelefonSatis.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly DataBaseContex _context;

        public AdminPanelController(DataBaseContex context)
        {
            _context = context;
        }

        public  ActionResult UserManagment()
        {
            return View(_context.Users.ToList());
        }
        public IActionResult PhoneManagment()
        {
            return View(_context.Phones.ToList());
        }
        public IActionResult BrandManagment()
        {
            return View(_context.Brands.ToList());
        }
        public IActionResult CommentManagment()
        {
            return View(_context.Comments.ToList());
        }
   
    }
}

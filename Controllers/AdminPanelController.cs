using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TelefonSatis.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult UserManagment()
        {
            return View();
        }
        public IActionResult PhoneManagment()
        {
            return View();
        }
        public IActionResult BrandManagment()
        {
            return View();
        }
        public IActionResult CommentManagment()
        {
            return View();
        }
   
    }
}

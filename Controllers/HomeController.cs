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

        public async Task<IActionResult> Detail(int? id)
        {

            ICollection <Brand> personlist = _context.Brands.ToList();
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

        [HttpPost]
        public async Task<IActionResult> Detail(int? id,int Score, string Comment,int PhoneId, int UserId)
        {
            int _score      = Score;
            string _comment = Comment;
            int _phoneId    = PhoneId;
            int _userId     = UserId;
            var phoneDetail = _context.Phones.Select(Phone => Phone).Where(p => p.PhoneId == _phoneId);
            int phoneDetailTotalScore = 0;
            int phoneDetailTotalPeople = 0;
            int score = 0;
            foreach (var item in phoneDetail)
            {
                phoneDetailTotalPeople = Int32.Parse(item.TotalPeople);
                phoneDetailTotalScore = Int32.Parse(item.TotalScore);

            }

            phoneDetailTotalPeople = phoneDetailTotalPeople + 1;
            phoneDetailTotalScore = phoneDetailTotalScore + _score;
            score = phoneDetailTotalScore / phoneDetailTotalPeople;

            
            Phone phoneCommentEdit = _context.Phones.SingleOrDefault(k => k.PhoneId == _userId);
            
            phoneCommentEdit.TotalPeople = phoneDetailTotalPeople.ToString();
            phoneCommentEdit.TotalScore = phoneDetailTotalScore.ToString();
            phoneCommentEdit.Score =  score.ToString();
            _context.SaveChanges();

            Comment com = new Comment
            {
                PhoneId = _phoneId,
                UserId = _userId,
                UserComment = _comment
            };
            _context.Comments.Add(com);
            _context.SaveChanges();

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

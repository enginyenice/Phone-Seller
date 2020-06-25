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

        public async Task<IActionResult> Cart()
        {
            var cartData = HttpContext.Session.GetString("CartData");
            if(cartData != null && cartData != "")
            {
                List<Phone> phoneList = new List<Phone>();
                string[] parcala = cartData.Split(",");
                
                for(int i = 0; i< parcala.Length -1; i++)
                {
                    var data = _context.Phones.Where(p => p.PhoneId == Int32.Parse(parcala[i])).SingleOrDefault();
                    phoneList.Add(data);
                }

                ViewBag.data = phoneList;
            } else
            {
                ViewBag.data = null;
            }
            return View();
        }


        public async Task<IActionResult> Products(int? id = 0,string search="")
        {
            var brand = _context.Brands
                          .Select(b => b);
            if (search == "")
            { 
           
            ICollection<Phone> phoneList;
            if (id != 0)
            {
               phoneList = _context.Phones.Where(p => p.BrandId == id).ToList();
            }else
            {
              phoneList = _context.Phones.ToList();
                
            }
            ViewBag.data = phoneList;
            
            } else
            {
                ICollection<Phone> phoneList;
                phoneList = _context.Phones.Where(p => p.Name.Contains(search)).ToList();
                ViewBag.data = phoneList;
            }
            return View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> CartItemDelete(int PhoneId)
        {


            var cartData = HttpContext.Session.GetString("CartData");
            if (cartData != null)
            {
                string data = "";
                List<Phone> phoneList = new List<Phone>();
                string[] parcala = cartData.Split(",");
                int index = -1;
                if(parcala.Length > 1)
                {
                    for (int i = 0; i < parcala.Length - 1; i++)
                    {
                        if (Int32.Parse(parcala[i]) == PhoneId)
                        {
                            index = i;
                        }
                    }
                    parcala = parcala.Where(val => val != PhoneId.ToString()).ToArray();

                    
                    for (int i = 0; i < parcala.Length - 1; i++)
                    {
                        data += parcala[i] + ",";
                    }
                    HttpContext.Session.SetString("CartData", data);
                } else
                {
                    data = null;
                    HttpContext.Session.SetString("CartData", null);
                    HttpContext.Session.Remove("CartData");
                }
                
            }



                return RedirectToAction("Cart", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CartBuy()
        {
            
            HttpContext.Session.Remove("CartData");
            TempData["Buy"] = "Ürünler satın alındı..";


            return RedirectToAction("Cart", "Home");
        }



        [HttpPost]
        public async Task<IActionResult> DetailCartAdd(int? id,  int PhoneId)
        {


            var cartData = HttpContext.Session.GetString("CartData");

            cartData += PhoneId + ",";
            HttpContext.Session.SetString("CartData", cartData);

            return RedirectToAction("Index","Home");

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
                phoneDetailTotalPeople = item.TotalPeople;
                phoneDetailTotalScore = item.TotalScore;

            }

            phoneDetailTotalPeople += 1;
            phoneDetailTotalScore += _score;
            score = phoneDetailTotalScore / phoneDetailTotalPeople;

            
            Phone phoneCommentEdit = _context.Phones.SingleOrDefault(k => k.PhoneId == _phoneId);
            
            phoneCommentEdit.TotalPeople = phoneDetailTotalPeople;
            phoneCommentEdit.TotalScore = phoneDetailTotalScore;
            phoneCommentEdit.Score =  score;
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

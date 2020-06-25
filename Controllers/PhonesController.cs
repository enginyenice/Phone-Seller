using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TelefonSatis.Data;
using TelefonSatis.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;


namespace TelefonSatis.Controllers
{
    public class PhonesController : Controller
    {
        private readonly DataBaseContex _context;
        private readonly IHostingEnvironment _evrimoment;
        public PhonesController(DataBaseContex context, IHostingEnvironment evrimoment)
        {
            _context = context;
            _evrimoment = evrimoment;
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
            return false;
        }


        #region default
        // GET: Phones
        public async Task<IActionResult> Index()
        {
            var dataBaseContex = _context.Phones.Include(p => p.brand);
            return View(await dataBaseContex.ToListAsync());
        }

        // GET: Phones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.brand)
                .FirstOrDefaultAsync(m => m.PhoneId == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // GET: Phones/Create
        public IActionResult Create()
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId");
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneId,Name,Color,Price,MinDesc,Images,Description,TotalScore,TotalPeople,Score,BrandId")] Phone phone)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction("PhoneManagment", "AdminPanel");
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", phone.BrandId);
            return View(phone);
        }

        // GET: Phones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", phone.BrandId);
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhoneId,Name,Color,Price,MinDesc,Description,TotalScore,TotalPeople,Score,BrandId")] Phone phone)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != phone.PhoneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneExists(phone.PhoneId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("PhoneManagment", "AdminPanel");
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", phone.BrandId);
            return View(phone);
        }

        // GET: Phones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            } else
            {
                var phone = await _context.Phones.FindAsync(id);
                _context.Phones.Remove(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction("PhoneManagment", "AdminPanel");
            }
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var phone = await _context.Phones.FindAsync(id);
            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();
            return RedirectToAction("PhoneManagment", "AdminPanel");
        }

        private bool PhoneExists(int id)
        {
            return _context.Phones.Any(e => e.PhoneId == id);
        }
        #endregion



        #region Image
        // GET: Phones/Edit/5
        public async Task<IActionResult> Image(int? id)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", phone.BrandId);
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Image([Bind("PhoneId","ResimDosyasi")] Phone phone)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    string resimler = Path.Combine(_evrimoment.WebRootPath, "images");
                    if (phone.ResimDosyasi.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(resimler, phone.ResimDosyasi.FileName), FileMode.Create))
                        {
                            await phone.ResimDosyasi.CopyToAsync(fileStream);
                        }
                    }
                    var data = _context.Phones.Where(p => p.PhoneId == phone.PhoneId).SingleOrDefault();
                    data.Images = phone.ResimDosyasi.FileName;
                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneExists(phone.PhoneId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("PhoneManagment", "AdminPanel");
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", phone.BrandId);
            return RedirectToAction("PhoneManagment", "AdminPanel");
        }

        #endregion



    }
}

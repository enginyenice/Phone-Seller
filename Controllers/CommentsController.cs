using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TelefonSatis.Data;
using TelefonSatis.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace TelefonSatis.Controllers
{
    public class CommentsController : Controller
    {
        private readonly DataBaseContex _context;

        public CommentsController(DataBaseContex context)
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
            return false;
        }



            // GET: Comments
            public async Task<IActionResult> Index()
        {
            return View(await _context.Comments.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,PhoneId,UserId,UserComment")] Comment comment)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
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

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["PhoneId"] = new SelectList(_context.Phones, "PhoneId", "PhoneId", comment.PhoneId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,PhoneId,UserId,UserComment")] Comment comment)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != comment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.CommentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CommentManagment", "AdminPanel");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
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
                var comment = await _context.Comments.FindAsync(id);
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("CommentManagment", "AdminPanel");
            }

            

        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}

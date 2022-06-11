using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttendanceProject.Data;
using StudentAttendanceProject.Models;

namespace AttendanceProject.Controllers
{
    public class SessionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Session
        public async Task<IActionResult> Index()
        {
            return View(await _context.Session.ToListAsync());
        }

        // GET: Session/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionModel = await _context.Session
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessionModel == null)
            {
                return NotFound();
            }

            return View(sessionModel);
        }

        // GET: Session/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Session/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] SessionModel sessionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sessionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sessionModel);
        }

        // GET: Session/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionModel = await _context.Session.FindAsync(id);
            if (sessionModel == null)
            {
                return NotFound();
            }
            return View(sessionModel);
        }

        // POST: Session/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SessionModel sessionModel)
        {
            if (id != sessionModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionModelExists(sessionModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sessionModel);
        }

        // GET: Session/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionModel = await _context.Session
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessionModel == null)
            {
                return NotFound();
            }

            return View(sessionModel);
        }

        // POST: Session/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sessionModel = await _context.Session.FindAsync(id);
            _context.Session.Remove(sessionModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionModelExists(int id)
        {
            return _context.Session.Any(e => e.Id == id);
        }
    }
}

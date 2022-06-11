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
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendance
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Attendence.Include(a => a.Session).Include(a => a.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attendance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceModel = await _context.Attendence
                .Include(a => a.Session)
                .Include(a => a.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceModel == null)
            {
                return NotFound();
            }

            return View(attendanceModel);
        }

        // GET: Attendance/Create
        public IActionResult Create()
        {
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Id");
            return View();
        }

        // POST: Attendance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AttendanceDate,SessionId,TeacherId")] AttendanceModel attendanceModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendanceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", attendanceModel.SessionId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Id", attendanceModel.TeacherId);
            return View(attendanceModel);
        }

        // GET: Attendance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceModel = await _context.Attendence.FindAsync(id);
            if (attendanceModel == null)
            {
                return NotFound();
            }
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", attendanceModel.SessionId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Id", attendanceModel.TeacherId);
            return View(attendanceModel);
        }

        // POST: Attendance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AttendanceDate,SessionId,TeacherId")] AttendanceModel attendanceModel)
        {
            if (id != attendanceModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendanceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceModelExists(attendanceModel.Id))
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
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", attendanceModel.SessionId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Id", attendanceModel.TeacherId);
            return View(attendanceModel);
        }

        // GET: Attendance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceModel = await _context.Attendence
                .Include(a => a.Session)
                .Include(a => a.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceModel == null)
            {
                return NotFound();
            }

            return View(attendanceModel);
        }

        // POST: Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendanceModel = await _context.Attendence.FindAsync(id);
            _context.Attendence.Remove(attendanceModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceModelExists(int id)
        {
            return _context.Attendence.Any(e => e.Id == id);
        }
    }
}

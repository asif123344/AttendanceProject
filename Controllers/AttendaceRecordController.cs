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
    public class AttendaceRecordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendaceRecordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AttendaceRecord
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AttendaceRecord.Include(a => a.Attendance).Include(a => a.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AttendaceRecord/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendaceRecordModel = await _context.AttendaceRecord
                .Include(a => a.Attendance)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendaceRecordModel == null)
            {
                return NotFound();
            }

            return View(attendaceRecordModel);
        }

        // GET: AttendaceRecord/Create
        public IActionResult Create()
        {
            ViewData["AttendanceId"] = new SelectList(_context.Attendence, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id");
            return View();
        }

        // POST: AttendaceRecord/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AttendanceId,StudentId,IsPresent")] AttendaceRecordModel attendaceRecordModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendaceRecordModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AttendanceId"] = new SelectList(_context.Attendence, "Id", "Id", attendaceRecordModel.AttendanceId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", attendaceRecordModel.StudentId);
            return View(attendaceRecordModel);
        }

        // GET: AttendaceRecord/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendaceRecordModel = await _context.AttendaceRecord.FindAsync(id);
            if (attendaceRecordModel == null)
            {
                return NotFound();
            }
            ViewData["AttendanceId"] = new SelectList(_context.Attendence, "Id", "Id", attendaceRecordModel.AttendanceId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", attendaceRecordModel.StudentId);
            return View(attendaceRecordModel);
        }

        // POST: AttendaceRecord/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AttendanceId,StudentId,IsPresent")] AttendaceRecordModel attendaceRecordModel)
        {
            if (id != attendaceRecordModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendaceRecordModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendaceRecordModelExists(attendaceRecordModel.Id))
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
            ViewData["AttendanceId"] = new SelectList(_context.Attendence, "Id", "Id", attendaceRecordModel.AttendanceId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", attendaceRecordModel.StudentId);
            return View(attendaceRecordModel);
        }

        // GET: AttendaceRecord/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendaceRecordModel = await _context.AttendaceRecord
                .Include(a => a.Attendance)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendaceRecordModel == null)
            {
                return NotFound();
            }

            return View(attendaceRecordModel);
        }

        // POST: AttendaceRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendaceRecordModel = await _context.AttendaceRecord.FindAsync(id);
            _context.AttendaceRecord.Remove(attendaceRecordModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendaceRecordModelExists(int id)
        {
            return _context.AttendaceRecord.Any(e => e.Id == id);
        }
    }
}

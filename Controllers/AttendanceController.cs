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
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Name");
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
                return RedirectToAction("AddNewRecord", new { attendanceId = attendanceModel.Id });
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddNewRecord(int attendanceId)
        {
            AttendanceModel attendanceModel = _context.Attendence.Where(a => a.Id == attendanceId).FirstOrDefault();

            List<StudentModel> students = _context.Student.Where(a => a.SessionId == attendanceModel.SessionId).ToList();

            var recordList = new List<StudentAttendance>();
            foreach (var item in students)
            {
                recordList.Add(new StudentAttendance
                {
                    IsPresent = false,
                    StudentId = item.Id,
                    StudentName = item.Name
                });
            }
            ViewBag.AttendanceId = attendanceId;
            return View(recordList);
        }


        [HttpPost]
        public IActionResult AddNewRecord(List<StudentAttendance> attendance, int attendanceId)
        {
            foreach (var item in attendance)
            {
                AttendaceRecordModel record = new AttendaceRecordModel
                {
                    IsPresent = item.IsPresent,
                    StudentId = item.StudentId,
                    AttendanceId = attendanceId
                };

                _context.AttendaceRecord.Add(record);
                _context.SaveChanges();
            }
            return RedirectToAction("ShowData", new { attendanceId = attendanceId });
        }


        public IActionResult ShowData(int attendanceId)
        {
            return View(_context.Attendence.Include(a => a.AttendanceRecords).Where(a => a.Id == attendanceId).FirstOrDefault());
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

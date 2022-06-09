using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SessionModel> Session { get; set; }

        public DbSet<TeacherModel> Teacher { get; set; }

        public DbSet<StudentModel> Student { get; set; }

        public DbSet<AttendanceModel> Attendence { get; set; }

        public DbSet<AttendaceRecordModel> AttendaceRecord { get; set; }


    }

}

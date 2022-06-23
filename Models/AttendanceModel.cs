using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAttendanceProject.Models
{
    [Table("Session")]
    public class SessionModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    [Table("Teacher")]
    public class TeacherModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }

    [Table("Student")]
    public class StudentModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Session")]
        public int SessionId { get; set; }

        public SessionModel Session { get; set; }
    }



    [Table("Attendance")]
    public class AttendanceModel
    {
        public int Id { get; set; }

        public DateTime AttendanceDate { get; set; }

        [ForeignKey("Session")]
        public int SessionId { get; set; }

        public SessionModel Session { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }

        public TeacherModel Teacher { get; set; }

        public IList<AttendaceRecordModel> AttendanceRecords { get; set; }

    }

    [Table("AttendanceRecord")]
    public class AttendaceRecordModel
    {
        public int Id { get; set; }

        [ForeignKey("Attendance")]
        public int AttendanceId { get; set; }
        public AttendanceModel Attendance { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public StudentModel Student { get; set; }

        public bool IsPresent { get; set; }

    }
    public class StudentAttendance
    {
        public string StudentName { get; set; }
        public int StudentId { get; set; }

        public bool IsPresent { get; set; }
    }
}

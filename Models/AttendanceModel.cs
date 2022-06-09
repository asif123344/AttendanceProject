using System;
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
    }

    [Table("Attendance")]
    public class AttendaceRecordModel
    {

        [ForeignKey("Attendance")]
        public int Id { get; set; }

        public AttendanceModel Attendance { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public StudentModel Student { get; set; }

        public bool IsPresent { get; set; }

    }
}

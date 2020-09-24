using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    class Student
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Hometown { get; set; }
        public double GPA { get; set; }

        //public Student(int no, string id, string fullName, DateTime birthDate, string hometown, double gpa)
        //{
        //    No = no;
        //    ID = id;
        //    FullName = fullName;
        //    BirthDate = birthDate;
        //    Hometown = hometown;
        //    GPA = gpa;
        //}
    }
}

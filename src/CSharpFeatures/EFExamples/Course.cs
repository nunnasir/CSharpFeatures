using System;
using System.Collections.Generic;

namespace EFExamples
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Fees { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartData { get; set; }
        public List<Topic> Topics { get; set; }
        public List<CourseStudent> CourseStudents { get; set; }
    }

}

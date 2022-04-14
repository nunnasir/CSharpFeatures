using System;
using System.Collections.Generic;
using System.Linq;

namespace EFExamples
{
    class Program
    {
        private static readonly CourseDbContext _context = new();
        static void Main(string[] args)
        {
            //InsertCourse();
            //RetriveAllCourse();
            //UpdateCourse(1);
            //UpdateAllCourse();
            //DeleteSingleCourse(12);
            //DeleteMultipleCourse("");
            //UpdateCourseWithTopics(2);
        }

        private static void InsertCourse()
        {
            _context.Courses.Add(new Course
            {
                Name = "C# The Definitive Reference",
                Fees = 5000.90,
                IsActive = true,
                StartData = DateTime.Now.AddMonths(2)
            });

            _context.Courses.Add(new Course
            {
                Name = "Javascript Connectiong the Dot",
                Fees = 8900.80,
                IsActive = true,
                StartData = DateTime.Now.AddMonths(3)
            });

            _context.Courses.Add(new Course
            {
                Name = "Python Programming",
                Fees = 6900.80,
                IsActive = false,
                StartData = DateTime.Now.AddMonths(3)
            });

            _context.SaveChanges();
        }

        private static void RetriveAllCourse()
        {
            List<Course> courses = _context.Courses.ToList();
            //List<Course> courses =  _context.Courses.Where(x => x.IsActive == false).ToList();
            //List<Course> courses =  _context.Courses.Where(x => x.Name.Contains("javascript")).ToList();

            foreach (var item in courses)
            {
                Console.WriteLine("Id: " + item.Id + " | Title: " + item.Name + " | IsActive: " + item.IsActive);
            }
        }

        private static void UpdateCourse(int id)
        {
            Course course = _context.Courses.FirstOrDefault(x => x.Id == id);
            if (course != null)
            {
                course.Name = "Python";
                course.Fees = 200;
                course.IsActive = true;

                _context.SaveChanges();
            }
        }

        private static void UpdateAllCourse()
        {
            List<Course> courses = _context.Courses.ToList();

            for (int i = 0; i < courses.Count; i++)
            {
                courses[i].Fees = 500;
                courses[i].StartData = DateTime.Now.AddDays(7);
            }

            _context.SaveChanges();
        }

        private static void DeleteSingleCourse(int id)
        {
            Course course = _context.Courses.FirstOrDefault(x => x.Id == id);

            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            _context.SaveChanges();
        }

        private static void DeleteMultipleCourse(string name)
        {
            // Contains Working as case insensitive way
            //List<Course> courses = _context.Courses.Where(x => x.Name.Contains(name) && x.IsActive == false).ToList();
            List<Course> courses = _context.Courses.Where(x => x.Fees > 500).ToList();

            if (courses.Count > 0)
            {
                _context.Courses.RemoveRange(courses);
            }

            _context.SaveChanges();
        }

        // Multiple Table With Join
        private static void UpdateCourseWithTopics(int id)
        {
            Course course = _context.Courses.FirstOrDefault(x => x.Id == id);
            if (course != null)
            {
                course.Topics = new List<Topic>
                {
                    new Topic{ Title = "Orientation" },
                    new Topic{ Title = "Fundamentals" },
                };

                _context.SaveChanges();
            }
        }
    }
}

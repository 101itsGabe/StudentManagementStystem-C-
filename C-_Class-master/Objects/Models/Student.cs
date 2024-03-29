﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects.Models
{
    public class Student : Person
    {
        public PersonClassification Classification { get; set; }
        public List<Submission> Submissions { get; set; }
        public Dictionary<string, List<Assignment>> Grades { get; set; }
        public Dictionary <string, decimal> CourseGrade { get; set; }
        public List<Course> Courses { get; set; }


        public Student() 
        {
            Grades = new Dictionary<string, List<Assignment>>();
            Submissions = new List<Submission>();
            Courses = new List<Course>();
            CourseGrade= new Dictionary<string, decimal>();
            
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Classification}";
        }

        /*
        public int CalulateGradePerClass()
        {

        }
        */
    }
}

public enum PersonClassification
{
    Freshman = 0,
    Sophmore = 1,
    Junior = 2,
    Senior = 3
}




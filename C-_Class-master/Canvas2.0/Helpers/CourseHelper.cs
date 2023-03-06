﻿using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using MyApp;
using Objects.Models;
using Objects.Services;

namespace Canvas2._0.Helpers
{
	public class CourseHelper
    {
        private CourseService cs;
        private StudentService sh;
        private ListNavigator<Course> courseNavigator;
        public CourseHelper()
        {
            sh = StudentService.Current;
            cs = CourseService.Current;
            courseNavigator = new ListNavigator<Course>(cs.courseList);
        }

        public void AddOrUpdateCourse(Course? course = null)
		{

            bool isCreate = false;
            if (course == null)
            {
                isCreate = true;
                course = new Course();
            }

            var choice = "Y";
            if (!isCreate)
            {
                Console.WriteLine("Do you want to update the couse code? Y N");
                choice = Console.ReadLine() ?? "N";
            }
            else
                choice = "Y";

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                bool redo = true;
                while (redo)
                {
                    Console.WriteLine("Class Code: ");
                    var cc = Console.ReadLine() ?? string.Empty;
                    if (!cs.courseList.Any())
                    {
                        course.classCode = cc;
                        redo = false;
                    }
                    else
                    {

                        if (cs.courseList.FindIndex(c => c.classCode == cc) == -1)
                        {
                            course.classCode = cc;
                            redo = false;
                        }
                        else
                            Console.WriteLine("Sorry a class with that code exsists try again...");

                    }
                }
                

                
            }

            if (!isCreate)
            {
                Console.WriteLine("Do you want to update the couse name? Y N");
                choice = Console.ReadLine() ?? "N";
            }
            else
                choice = "Y";

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Course Name: ");
                course.Name = Console.ReadLine() ?? string.Empty;
            }

            if (!isCreate)
            {
                Console.WriteLine("Do you want to update the couse descrption? Y N");
                choice = Console.ReadLine() ?? "N";
            }
            else
                choice = "Y";

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Enter a Description: ");
                course.Description = Console.ReadLine() ?? string.Empty;
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Credit Hours: ");
                var credit = Console.ReadLine() ?? string.Empty;
                int.TryParse(credit, out int creditHours);
                course.creditHours = creditHours;
            }



                if (isCreate)
            {
                var roster = new List<Student>();
                var assign = new List<Assignment>();
                var assignmentgroup = new List<AssignmentGroup>();

                Console.WriteLine("Which Students should be enrolled in this course? 'Q' to quit: ");
                bool continueAdding = true;



                while (continueAdding)
                {
                    sh.Students.Where(s => !roster.Any(sn => s.Name.ToUpper() == sn.Name.ToUpper())).ToList().ForEach(Console.WriteLine);

                    var studName = "Q";
                    if (sh.Students.Any(s => !roster.Any(sn => s.Name.ToUpper() == sn.Name.ToUpper())))
                    {
                        studName = Console.ReadLine() ?? string.Empty;
                        roster.Add(sh.Students.FirstOrDefault(s => s.Name.ToUpper() == studName.ToUpper()));
                        Console.WriteLine('\n');
                    }

                    if (studName.Equals("Q") || !sh.Students.Any(s => !roster.Any(sn => s.Name.ToUpper() == sn.Name.ToUpper())))
                    {
                        continueAdding = false;
                    }

                }

                Console.WriteLine("Woudl you like to Add Assignment Groups? Y N");
                var ag = Console.ReadLine() ?? string.Empty;
                if (ag.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    continueAdding = true;
                    while (continueAdding)
                    {
                        
                        Console.WriteLine("Assignment Group Name: ");
                        var aName = Console.ReadLine() ?? string.Empty;

                        
                        Console.WriteLine("Add more assigments groups? Y N:");
                        ag = Console.ReadLine() ?? "N";
                        if (ag.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                        {
                            continueAdding = false;
                        }
                        
                        assignmentgroup.Add(new AssignmentGroup{ Name = aName});



                        
                    }

                    Console.WriteLine("Set the weights for each of the classes");
                    int l = assignmentgroup.Count();
                    int curPercent = 100;           //Current precent of class wight
                    List<AssignmentGroup?> tempList = new List<AssignmentGroup>(assignmentgroup);
                    while (tempList.Any()) 
                    {
                        Console.WriteLine($"Current Percent: {curPercent}%");
                        tempList.ForEach(g => Console.WriteLine(g.Name));
                        Console.WriteLine("Chose a Group to add a weight: ");
                        var group = Console.ReadLine() ?? string.Empty;
                        var curGroup = tempList.FirstOrDefault(g => g.Name == group);
                        Console.WriteLine("Enter a Percent Weight: ");
                        var w = Console.ReadLine() ?? string.Empty;
                        decimal.TryParse(w, out decimal curWieght);
                        if(curGroup != null)
                            curGroup.weight = curWieght;
                        curPercent -= (int)curWieght;
                        tempList.Remove(curGroup);

                        
                    }
                }

                Console.WriteLine("Would you like to add assigments? Y N: ");
                var ar = Console.ReadLine() ?? "N";
                if (ar.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    continueAdding = true;
                    while (continueAdding)
                    {
                        //ID
                        Console.WriteLine("ID: ");
                        var aId = Console.ReadLine() ?? string.Empty;
                        if (!int.TryParse(aId, out int aInt))
                        {
                            Console.WriteLine("Couldnt Hack it");
                        }
                        //Name
                        Console.WriteLine("Name: ");
                        var aname = Console.ReadLine() ?? string.Empty;
                        //Description
                        Console.WriteLine("Description: ");
                        var adesc = Console.ReadLine() ?? string.Empty;
                        //TotalPoints
                        Console.WriteLine("Total Points: ");
                        var tp = decimal.Parse(Console.ReadLine() ?? "100");
                        //DueDate
                        Console.WriteLine("Due Date: ");
                        var dd = DateOnly.Parse(Console.ReadLine() ?? "01/01/1900");

                        var curAssign = new Assignment
                        {
                            Name = aname,
                            Description = adesc,
                            totalPoints = tp,
                            dueDate = dd,
                            Id = aInt,
                        };

                        assign.Add(curAssign);

                        Console.WriteLine("Which Group would you like to add this Assignment to?");
                        assignmentgroup.ForEach(a => Console.WriteLine(a.Name));
                        ar = Console.ReadLine() ?? string.Empty;
                        var curGroup = assignmentgroup.FirstOrDefault(a => a.Name.ToUpper() == ar.ToUpper());
                        if(curGroup == null) { Console.WriteLine("Oh Brother");  }
                        if (curGroup != null)
                        {
                            curAssign.AssignedGroup = curGroup.Name;
                            curGroup.AddAssignment(curAssign);
                            curGroup.totalPoints += curAssign.totalPoints;
                            Console.WriteLine(curGroup.totalPoints.ToString());
                        }


                        Console.WriteLine("Add more assigments? Y N:");
                        ar = Console.ReadLine() ?? "N";
                        Console.WriteLine(ar);
                        if (ar.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                        {
                            continueAdding = false;
                        }
                    }
                }

                course.Roster = new List<Student>();
                course.Roster.AddRange(roster);
                course.Assignments = new List<Assignment>();
                course.Assignments.AddRange(assign);
                course.AssignmentGroups= new List<AssignmentGroup>();
                course.AssignmentGroups.AddRange(assignmentgroup);

                foreach(var s in course.Roster)
                {
                    if(s != null)
                        s.Grades.Add(course.classCode, course.Assignments);
                };

                

            }


            if (isCreate)
                cs.Add(course);

        }


        public void NavigateCourses()
        {
            bool keepPaging = true;
            while (keepPaging)
            {
                foreach (var pair in courseNavigator.GetCurrentPage())
                {
                    Console.WriteLine($"{pair.Key}. {pair.Value}");
                }

                if (courseNavigator.HasPreviousPage)
                {
                    Console.WriteLine("P. Previous Page");
                }

                if (courseNavigator.HasNextPage)
                {
                    Console.WriteLine("N. Next Page");
                }

                Console.WriteLine("Make a Selection");
                var selectionChoice = Console.ReadLine();
                if ((selectionChoice?.Equals("P", StringComparison.InvariantCultureIgnoreCase) ?? false)
                   || (selectionChoice?.Equals("N", StringComparison.InvariantCultureIgnoreCase) ?? false))
                {
                    //Navigate Through Pages
                    if (selectionChoice.Equals("P", StringComparison.InvariantCultureIgnoreCase))
                        courseNavigator.GoBackward();
                    else if (selectionChoice.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                        courseNavigator.GoForward();
                }
                else
                {
                    Console.WriteLine("Select a Student");
                    var curStud = Console.ReadLine() ?? string.Empty;
                    keepPaging = false;
                }
            }
        }

        public void ListAllCourses()
        {
            NavigateCourses();
        }




        public void SearchCourse()
        { 

            Console.WriteLine("Select a course: ");
            var code = Console.ReadLine() ?? string.Empty;

            var curCourse = cs.courseList.FirstOrDefault(c => c.classCode.Equals(code,StringComparison.InvariantCultureIgnoreCase));

            if(curCourse != null ) 
            {
                Console.WriteLine($"{curCourse.Name}");
                Console.WriteLine($"{curCourse.classCode}");
                Console.WriteLine("Roster:");
                curCourse.Roster.ForEach(Console.WriteLine);
                Console.WriteLine("Assignment Groups");
                if (!curCourse.AssignmentGroups.Any()) { Console.WriteLine("AHHHH"); }
                curCourse.AssignmentGroups.ForEach(a => Console.WriteLine(a.Name));
                //Console.WriteLine(curCourse.DetailDisplay);
            }
        }

        public void UpdateCourseRecord()
        {
            Console.WriteLine("Enter the course code: ");
            var n = Console.ReadLine() ?? string.Empty;
            SearchCourse();

            var curCourse = cs.courseList.FirstOrDefault(s => s.classCode.ToUpper().Contains(n.ToUpper()));

            if (curCourse == null)
            {
                    AddOrUpdateCourse(curCourse);
            }
            

        }

        public void RemoveStudentFromCourse()
        {
            Console.WriteLine("Enter class code: ");
            var c = Console.ReadLine() ?? string.Empty;
            var curCourse = cs.courseList.FirstOrDefault(s => s.classCode.Contains(c));
            if (curCourse == null)
                Console.WriteLine($"{c} not found");
            else
            {
                bool isRemoving = true;
                while(isRemoving)
                { 
                    curCourse.Roster.ToList().ForEach(Console.WriteLine);
                    Console.WriteLine("Enter the student name you would like to remove 'Q' to quit: ");
                    var n = Console.ReadLine() ?? string.Empty;

                    if(n == "Q")
                    {
                        isRemoving = false;
                    }

                    else
                    {
                        var curStudent = sh.Students.FirstOrDefault(s => s.Name.ToUpper().Contains(n.ToUpper()));
                        if (curStudent == null) { Console.WriteLine("Student not found"); }
                        else
                        {
                            curCourse.Roster.Remove(curStudent);
                        }
                    }
                }
            }

        }

        /*
        public void EnterNewSubmission()
        {
            Console.WriteLine("Enter Couse ID");
            var cid = Console.ReadLine() ?? string.Empty;
            var c = cs.GetCourse(cid);
            if( c == null)
            {
                Console.WriteLine($"No Course with the Id: {cid}");
                return;
            }

            c.Roster.ForEach(Console.WriteLine);
            Console.WriteLine("Enter Student Name: ");
            var n = Console.ReadLine() ?? string.Empty;
            var curStudent = sh.GetStudent(n);
            Console.WriteLine("Enter Assignment ID: ");
            var aId = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(aId, out int aId2))
            {
                Console.WriteLine("AHH");
            }
            Console.WriteLine("Enter Submission ID: ");
            var sId = Console.ReadLine() ?? string.Empty;
            if(!int.TryParse(sId, out int sId2))
            {
                Console.WriteLine("AHH");
            }
            Assignment curAssign;
            curAssign = c.Assignments.FirstOrDefault(a => a.Id == aId2);
            if( curAssign == null )
            {
                Console.WriteLine("Sorry no assignment with that id");
                return;
            }
            var submission = new Submission(sId2, n, curAssign, c.classCode);
            if (curStudent != null)
            {
                curStudent.Submissions.Add(submission);
                Console.WriteLine("Enter points on assignment: ");
                var points = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(points, out int points2))
                {
                    curStudent.Grades.Add(submission.Id, submission.getGrade(curAssign, points2));
                    curStudent.Submissions.Add(submission);
                }
            }
        }
        */

        public void GiveGrade()
        {
            cs.courseList.ForEach(c => Console.WriteLine(c.classCode));
            Console.WriteLine("Class Code:");
            var cc = Console.ReadLine() ?? string.Empty;
            var curCourse = cs.GetCourse(cc);
            curCourse.Roster.ForEach(Console.WriteLine);
            Console.WriteLine("Student Name: ");
            var n = Console.ReadLine() ?? string.Empty;
            curCourse.Assignments.ForEach(a => Console.WriteLine($"{a.Name} - {a.Id}"));
            Console.WriteLine("Assignment ID:");
            var aId = Console.ReadLine() ?? string.Empty;
            int.TryParse(aId, out int assignID);
            var cAssign = curCourse.Assignments.FirstOrDefault(a => a.Id == assignID);
            Console.WriteLine($"Total Points: {cAssign.totalPoints}");
            Console.WriteLine("Enter the grade (decimcal):");
            var g = Console.ReadLine() ?? string.Empty;
            decimal.TryParse(g, out decimal grade);
            cs.giveGrade(cc, n, assignID, grade);


        }

        public void CreateAnnouncement()
        {
            cs.courseList.ForEach(cs=> Console.WriteLine(cs.classCode));
            Console.WriteLine("Class Code");
            var cCode = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Announcement Name: ");
            var aName = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Announcement Description: ");
            var aDesc = Console.ReadLine() ?? string.Empty;
            cs.CreateAndUpdateAnnouncement(cCode,aName, aDesc);
        }

        public void ShowAnnouncement()
        {
            cs.courseList.ForEach(cs => Console.WriteLine(cs.classCode));
            Console.WriteLine("Class Code");
            var cCode = Console.ReadLine() ?? string.Empty;
            var curCourse = cs.GetCourse(cCode);
            if(curCourse != null) 
                curCourse.Announcements.ForEach(a => Console.WriteLine(a.Name));
            Console.WriteLine("Select an Announcement: ");
            var aName = Console.ReadLine() ?? string.Empty;
            var curAnnon =  curCourse.Announcements.FirstOrDefault(a => a.Name == aName);
            Console.WriteLine(curAnnon.Description);
        }

        public void DeleteAnnouncement()
        {
            cs.courseList.ForEach(cs => Console.WriteLine(cs.classCode));
            Console.WriteLine("Class Code");
            var cCode = Console.ReadLine() ?? string.Empty;
            var curCourse = cs.GetCourse(cCode);
            if (curCourse != null)
                curCourse.Announcements.ForEach(a => Console.WriteLine(a.Name));
            Console.WriteLine("Select an Announcement: ");
            var aName = Console.ReadLine() ?? string.Empty;
            cs.DeleteAnnouncement(cCode, aName);
        }



    }
}


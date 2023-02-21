﻿using System;
using System.Collections.Specialized;
using Canvas2._0.Helpers;
using Objects.Models;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool cont = true;

            //Having this list to null is VERY BAD
            //Beacuse you dont control it you cant verify if its null
            //IF it is null everything will EXPLODE!!!

            List<Course> courseList = new List<Course>();
            var StudHelp = new StudentHelper();
            var CourseHelp = new CourseHelper();

            while (cont)
            {
                Console.WriteLine("Choose an option: ");
                Console.WriteLine("1. Create a Course");
                Console.WriteLine("2. Create a Student");
                Console.WriteLine("3. Add a student to a course");
                Console.WriteLine("4. Remove a student to a course");
                Console.WriteLine("5. Search for a course");
                Console.WriteLine("6. List all courses");
                Console.WriteLine("7. Search for a student");
                Console.WriteLine("8. List all students");
                Console.WriteLine("9. List all courses a student is taking");
                Console.WriteLine("10. Update a courses information");
                Console.WriteLine("11. Update a student information");
                Console.WriteLine("12. Create an assignment and add it to the list for a course");
                Console.WriteLine("13. Exit");

                string choice = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(choice, out int choiceInt))
                {
   

                    //Create a course
                    if (choiceInt == 1)
                    {
                        CourseHelp.AddOrUpdateCourse();
                    }

                    //Create a Student
                    else if (choiceInt == 2)
                    {
                        StudHelp.AddOrUpdateStudent();
                    }

                    //Add a student to a course
                    else if (choiceInt == 3)
                    {
                        CourseHelp.AddStudentToRoster();

                    }


                    /*
                    //Remove a student from a course
                    else if (choiceInt == 4)
                    {
                        Course curCourse = new Course();
                        Console.WriteLine("\nWhich Student would you like to remove?: ");
                        var studentString = Console.ReadLine() ?? string.Empty; ;
                        Console.WriteLine("\nEnter the class code: ");
                        var courseCode = Console.ReadLine() ?? string.Empty;

                        foreach (Course c in courseList)
                        {
                            if (courseCode == c.classCode)
                            {
                                curCourse = c;
                                break;
                            }
                        }


                        foreach (Person p in curCourse.roster)
                        {
                            if (studentString == p.Name)
                            {
                                curCourse.roster.Remove(p);
                                p.courses.Remove(curCourse);
                                break;
                            }
                        }



                    }


                    //Search for a course
                    else if (choiceInt == 5)
                    {
                        Console.WriteLine("Enter a class code: ");
                        var curCode = Console.ReadLine() ?? string.Empty;

                        foreach (Course c in courseList)
                        {
                            if (curCode == c.classCode)
                            {
                                Console.WriteLine("\nClass: " + c.Name);
                                Console.WriteLine("Class Code: " + c.classCode);
                                Console.WriteLine("Description: " + c.Description);
                                Console.WriteLine("\nStudents in this class: ");

                                Console.WriteLine(c.ShowRoster());

                                Console.WriteLine("\n");
                                Console.WriteLine("Assignments:");
                                foreach (Assignment a in c.assignments)
                                {
                                    Console.WriteLine("\n");
                                    Console.WriteLine(a.Name);
                                    Console.WriteLine(a.Description);
                                    Console.WriteLine(a.totalPoints);
                                    Console.WriteLine(a.dueDate);
                                    Console.WriteLine("\n");
                                }
                                break;
                            }
                        }


                    }
                    */

                    //List all courses
                    else if (choiceInt == 6)
                    {
                        CourseHelp.ListCourses();
                    }


                    //Search for a student
                    else if (choiceInt == 7)
                    {
                        StudHelp.SearchStudents();
                    }

                    //List all students
                    else if (choiceInt == 8)
                    {
                        StudHelp.ListStudents();
                    }

                    
                    //List all courses a student is taking
                    else if (choiceInt == 9)
                    {
                        Console.WriteLine("Enter the students name: ");
                        var name = Console.ReadLine();
                        foreach (Person p in personList)
                        {
                            if (name == p.Name)
                            {
                                foreach (Course c in p.courses)
                                {
                                    Console.WriteLine(c.Display);
                                }
                                break;
                            }
                        }
                    }

                    //Updtae course information
                    else if (choiceInt == 10)
                    {
                        CourseHelp.AddOrUpdateCourse();
                    }

                    //Update a students information
                    else if (choiceInt == 11)
                    {
                        StudHelp.AddOrUpdateStudent();
                    }

                    //Create an assignment and add it to the courses assignment
                    else if (choiceInt == 12)
                    {
                        Console.WriteLine("Enter the class code you would like to add the assignment to: ");
                        var code = Console.ReadLine() ?? string.Empty;

                        Assignment newAssignment = new Assignment();
                        Console.WriteLine("Enter the name of the new assignment");
                        newAssignment.Name = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Enter the description of the new assignment");
                        newAssignment.Description = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Enter the total points the assignment will be worth: ");
                        var tp = Console.ReadLine() ?? string.Empty;
                        if (int.TryParse(tp, out var value))
                        {
                            newAssignment.totalPoints = value;
                        }

                        Console.WriteLine("Enter the due date of the assignment: ");
                        newAssignment.dueDate = DateOnly.Parse(Console.ReadLine() ?? string.Empty);

                        foreach (Course c in courseList)
                        {
                            c.assignments.Add(newAssignment);
                        }

                    }


                    else if (choiceInt == 13)
                    {
                        cont = false;
                    }
                    */

                }
            }
        }
    }
}


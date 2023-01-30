﻿using System;
using System.Collections.Specialized;
//Fully Qualified Name (FQN)
using Library.TaskManagement.Models;

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

            List<Item> taskList = new List<Item>();
            List<Course> courseList = new List<Course>();
            List<Person> personList = new List<Person>();

            while (cont)
            {
                Console.WriteLine("Choose an option: ");
                Console.WriteLine("1. Add a Task");
                Console.WriteLine("2. List all Tasks");
                Console.WriteLine("3. Seacrh for Tasks");
                Console.WriteLine("4. Create a Course");
                Console.WriteLine("5. Create a Student");
                Console.WriteLine("6. Add a student to a course");
                Console.WriteLine("7. Remove a student to a course");
                Console.WriteLine("8. Search for a course");
                Console.WriteLine("9. List all courses");
                Console.WriteLine("10. Search for a student");
                Console.WriteLine("11. List all students");
                Console.WriteLine("12. List all courses a student is taking");
                Console.WriteLine("13. Update a courses information");
                Console.WriteLine("14. Update a student information");
                Console.WriteLine("15. Create an assignment and add it to the list for a course");
                Console.WriteLine("16. Exit");

                string choice = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(choice, out int choiceInt))
                {
                    //Add a task
                    if (choiceInt == 1)
                    {
                        var isToDo = true;
                        Console.WriteLine("Is this a Calander Appointment?(T/C)");
                        var response = Console.ReadLine() ?? string.Empty;
                        //If there is a Y character that has an accent mark it will still be Y
                        //and ignores capitals
                        if (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            isToDo = false;
                        }

                        var newTask = new Item();

                        if (isToDo)
                        {
                            var newToDo = newTask as ToDo;
                            newToDo.IsComplete = false;
                            taskList.Add(newToDo);
                        }
                        else
                        {
                            var newAppointment = newTask as Appointment;
                            newAppointment.Start = DateTime.Now;
                            newAppointment.End = DateTime.Now.AddHours(1);
                            taskList.Add(newAppointment);
                        }
                        Console.WriteLine("Enter a name: ");
                        newTask.Name = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Enter a Description: ");
                        newTask.Description = Console.ReadLine() ?? string.Empty;
                        taskList.Add(newTask);

                    }

                    //List all tasks
                    else if (choiceInt == 2)
                    {
                        taskList.ForEach(number => Console.WriteLine(number));
                    }

                    //Search for tasks
                    else if (choiceInt == 3)
                    {
                        Console.WriteLine("Enter the seacrh term: ");
                        var query = Console.ReadLine();

                        var FilteredTasks = taskList
                            .Where(t =>
                            ((t is Appointment) || ((t is ToDo) && (t as ToDo).IsComplete)) &&
                            t.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase)
                            || t.Description.Contains(query, StringComparison.InvariantCultureIgnoreCase)
                            );

                    }

                    //Create a course
                    else if (choiceInt == 4)
                    {
                        var newCourse = new Course();
                        Console.WriteLine("Course Name: ");
                        newCourse.Name = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Course Code: ");
                        var code = Console.ReadLine() ?? string.Empty;
                        newCourse.classCode = code;

                        Console.WriteLine("Course Description: ");
                        newCourse.Description = Console.ReadLine() ?? string.Empty;
                        courseList.Add(newCourse);
                    }

                    //Create a Student
                    else if (choiceInt == 5)
                    {
                        var newPerson = new Person();
                        Console.WriteLine("Person Name: ");
                        newPerson.Name = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Person Classification: ");
                        newPerson.classification = Console.ReadLine() ?? string.Empty;
                        personList.Add(newPerson);
                    }

                    //Add a student to a course
                    else if (choiceInt == 6)
                    {
                        Person curPerson = new Person();
                        Console.WriteLine("Which Student would you like to add?: ");
                        var studentString = Console.ReadLine() ?? string.Empty; ;
                        Console.WriteLine("Enter the class code: ");
                        var courseCode = Console.ReadLine() ?? string.Empty;

                        foreach (Person p in personList)
                        {
                            if (p.Name.Contains(studentString, StringComparison.InvariantCultureIgnoreCase))
                            {
                                curPerson = p;
                                break;
                            }
                        }
                        if (curPerson == null)
                            Console.WriteLine("Sorry we couldnt find " + studentString);


                        foreach (Course c in courseList)
                        {
                            if (c.classCode == courseCode)
                            {
                                if (curPerson != null)
                                {
                                    c.roster = new List<Person>();
                                    Console.WriteLine("CurPerson Name:  " + curPerson.Name);
                                    c.roster.Add(curPerson);
                                    Console.WriteLine("Successfully added " + curPerson.Name + " to " + c.Name);
                                }
                            }
                        }

                        if (courseCode == "0")
                            Console.WriteLine("Could not find Course with code: " + courseCode);

                    }


                    //Remove a student from a course
                    else if (choiceInt == 7)
                    {
                        Course curCourse = new Course();
                        Console.WriteLine("Which Student would you like to remove?: ");
                        var studentString = Console.ReadLine() ?? string.Empty; ;
                        Console.WriteLine("Enter the class code: ");
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
                                break;
                            }
                        }

                    }


                    //Search for a course
                    else if (choiceInt == 8)
                    {
                        Console.WriteLine("Enter a class code: ");
                        var classCode = Console.ReadLine() ?? string.Empty;

                        foreach (Course c in courseList)
                        {
                            if (classCode == c.classCode)
                            {
                                Console.WriteLine("Class: " + c.Name);
                                Console.WriteLine("Class Code: " + c.classCode);
                                Console.WriteLine("Description: " + c.Description);
                                break;
                            }
                        }
                    }

                    //List all courses
                    else if (choiceInt == 9)
                    {
                        Console.WriteLine("All Courses: ");
                        if (courseList.Count() == 0)
                        {
                            Console.WriteLine("There are no courses yet.");
                        }
                        else
                        {
                            foreach (Course c in courseList)
                            {
                                Console.WriteLine("Couse: " + c.Name);
                                Console.WriteLine("Class Code: " + c.classCode);
                                Console.WriteLine("Description: " + c.Description);
                                Console.WriteLine("\n");
                            }
                            Console.WriteLine("\n");
                        }
                    }


                    //Search for a student
                    else if (choiceInt == 10)
                    {
                        Console.WriteLine("Enter a Students name: ");
                        var name = Console.ReadLine() ?? string.Empty;

                        foreach (Person p in personList)
                        {
                            if (name.Equals(p.Name))
                            {
                                Console.WriteLine("Class: " + p.Name);
                                Console.WriteLine("Classification: " + p.classification);
                                break;
                            }
                        }
                    }

                    //List all students
                    else if (choiceInt == 11)
                    {
                        Console.WriteLine("All studemts: ");
                        if (personList.Count() == 0)
                        {
                            Console.WriteLine("There are no students yet.");
                        }
                        else
                        {
                            foreach (Person c in personList)
                            {
                                Console.WriteLine("Couse: " + c.Name);
                                Console.WriteLine("Class Code: " + c.classification + "\n");

                            }
                            Console.WriteLine("\n");
                        }
                    }


                    //List all courses a student is taking
                    else if (choiceInt == 12)
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
                    else if (choiceInt == 13)
                    {
                        Console.WriteLine("Enter the course code you would like to update: ");
                        var code = Console.ReadLine();
                        Course curCourse = null;

                        foreach (Course c in courseList)
                        {
                            if (c.classCode == code)
                            {
                                curCourse = c;
                                break;
                            }
                        }

                        Console.WriteLine("What would you like to update?");
                        Console.WriteLine("Name - N");
                        Console.WriteLine("Description - D");
                        Console.WriteLine("Course Code - C");
                        var ch = Console.ReadLine();
                        bool valid = false;

                        if (curCourse != null)
                        {
                            while (!valid)
                            {
                                switch (ch)
                                {
                                    case "C":
                                        Console.WriteLine("Enter the new Code you want to give to the course: ");
                                        curCourse.classCode = Console.ReadLine() ?? string.Empty;
                                        break;

                                    case "D":
                                        Console.WriteLine("Enter the new description you would like to give to the course: ");
                                        curCourse.Description = Console.ReadLine() ?? string.Empty;
                                        break;

                                    case "N":
                                        Console.WriteLine("Enter the new name you would like to give the class: ");
                                        curCourse.Name = Console.ReadLine() ?? string.Empty;
                                        break;



                                    default:
                                        valid = false;
                                        break;


                                }
                            }
                        }
                        else
                            Console.WriteLine("Sorry, there is no class with the code: " + code);
                    }

                    //Update a students information
                    else if (choiceInt == 14)
                    {
                        Console.WriteLine("Enter the name of the student you would like to update: ");
                        var name = Console.ReadLine();
                        Person curPerson = null;

                        foreach (Person p in personList)
                        {
                            if (p.Name == name)
                            {
                                curPerson = p;
                                break;
                            }
                        }

                        Console.WriteLine("What would you like to update?");
                        Console.WriteLine("Name - N");
                        Console.WriteLine("Classification - C");
                        var ch = Console.ReadLine();
                        bool valid = false;

                        if (curPerson != null)
                        {
                            while (!valid)
                            {
                                switch (ch)
                                {
                                    case "C":
                                        Console.WriteLine("Enter the new Code you want to give to the course: ");
                                        curPerson.classification = Console.ReadLine() ?? string.Empty;
                                        break;

                                    case "N":
                                        Console.WriteLine("Enter the new name you would like to give the person: ");
                                        curPerson.Name = Console.ReadLine() ?? string.Empty;
                                        break;



                                    default:
                                        valid = false;
                                        break;


                                }
                            }
                        }
                        else
                            Console.WriteLine("SOrry, there is no class with the code: " + name);
                    
                    }

                    //Create an assignment and add it to the courses assignment
                    else if (choiceInt == 15)
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
                        if(int.TryParse(tp, out var value)) 
                        {
                            newAssignment.totalPoints = value;
                        }

                        Console.WriteLine("Enter the due date of the assignment: ");
                        newAssignment.dueDate = DateOnly.Parse(Console.ReadLine() ?? string.Empty);

                    }
                                

                    else if (choiceInt == 16)
                            {
                                cont = false;
                            }
                                           
                }
            }
        }
    }
}
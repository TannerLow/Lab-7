﻿using DomainModel;
using BusinessLayer;
using System;
using System.Collections.Generic;

namespace ConsoleClient
{
    class Program
    {
        // Create a new business layer
        private static IBusinessLayer businessLayer = new BuinessLayer();

        static void Main(string[] args)
        {
            run();
        }

        /// <summary>
        /// Display the menu and get user selection until exit.
        /// </summary>
        public static void run()
        {
            bool repeat = true;
            int input;

            do
            {
                Menu.displayMenu();
                input = Validator.getMenuInput();

                switch (input)
                {
                    case 0:
                        repeat = false;
                        break;
                    case 1:
                        Menu.clearMenu();
                        addTeacher();
                        break;
                    case 2:
                        Menu.clearMenu();
                        updateTeacher();
                        break;
                    case 3:
                        Menu.clearMenu();
                        removeTeacher();
                        break;
                    case 4:
                        Menu.clearMenu();
                        listTeachers();
                        break;
                    case 5:
                        Menu.clearMenu();
                        listTeacherCourses();
                        break;
                    case 6:
                        Menu.clearMenu();
                        addCourse();
                        break;
                    case 7:
                        Menu.clearMenu();
                        updateCourse();
                        break;
                    case 8:
                        Menu.clearMenu();
                        removeCourse();
                        break;
                    case 9:
                        Menu.clearMenu();
                        listCourses();
                        break;
                    case 10:
                        Menu.clearMenu();
                        addCourseToTeacher();
                        break;
                    case 11:
                        Menu.clearMenu();
                        moveCourse();
                        break;
                }
            } while (repeat);
        }

        //CRUD for teachers

        /// <summary>
        /// Add a teacher to the database.
        /// </summary>
        public static void addTeacher()
        {
            Console.WriteLine("Enter a teacher name: ");
            string teacherName = Console.ReadLine();
            Teacher it = new Teacher() { TeacherName = teacherName };
            it.EntityState = EntityState.Added;
            businessLayer.AddTeacher(it);
            Console.WriteLine("{0} has been added to the database.", teacherName);
        }

        /// <summary>
        /// Update the name of a teacher.
        /// </summary>
        public static void updateTeacher()
        {
            Menu.displaySearchOptions();
            int input = Validator.getOptionInput();
            listTeachers();

            //Find by a teacher's name
            if (input == 1)
            {
                Console.WriteLine("Enter a teacher's name: ");
                Teacher teacher = businessLayer.GetTeacherByName(Console.ReadLine());
                if (teacher != null)
                {
                    Console.WriteLine("Change this teacher's name to: ");
                    teacher.TeacherName = Console.ReadLine();
                    teacher.EntityState = EntityState.Modified;
                    businessLayer.UpdateTeacher(teacher);
                }
                else
                {
                    Console.WriteLine("Teacher does not exist.");
                };
            }
            //find by a teacher's id
            else if (input == 2)
            {
                int id = Validator.getId();
                Teacher teacher = businessLayer.GetTeacherById(id);
                if (teacher != null)
                {
                    Console.WriteLine("Change this teacher's name to: ");
                    teacher.TeacherName = Console.ReadLine();
                    teacher.EntityState = EntityState.Modified;
                    businessLayer.UpdateTeacher(teacher);
                }
                else
                {
                    Console.WriteLine("Teacher does not exist.");
                };
            }
        }

        /// <summary>
        /// Remove a teacher from the database.
        /// </summary>
        public static void removeTeacher()
        {
            listTeachers();
            int id = Validator.getId();
            Teacher teacher = businessLayer.GetTeacherById(id);
            if (teacher != null)
            {
                Console.WriteLine("{0} has been removed.", teacher.TeacherName);
                teacher.EntityState = EntityState.Deleted;
                businessLayer.RemoveTeacher(teacher);
            }
            else
            {
                Console.WriteLine("Teacher does not exist.");
            };
        }

        /// <summary>
        /// List all teachers in the database.
        /// </summary>
        public static void listTeachers()
        {
            IList<Teacher> teachers = businessLayer.GetAllTeachers();
            foreach (Teacher teacher in teachers)
                Console.WriteLine("Teacher ID: {0}, Name: {1}", teacher.TeacherId, teacher.TeacherName);
        }

        /// <summary>
        /// List the courses of a specified teacher.
        /// </summary>
        public static void listTeacherCourses()
        {
            listTeachers();
            int id = Validator.getId();
            Teacher teacher = businessLayer.GetTeacherById(id);
            if (teacher != null)
            {
                Console.WriteLine("Listing courses for [ID: {0}, Name: {1}]:", teacher.TeacherId, teacher.TeacherName);
                if (teacher.Courses.Count > 0)
                {
                    foreach (Course course in teacher.Courses)
                        Console.WriteLine("Course ID: {0}, Name: {1}", course.CourseId, course.CourseName);
                }
                else
                {
                    Console.WriteLine("No courses for [ID: {0}, Name: {1}]:", teacher.TeacherId, teacher.TeacherName);
                };

            }
            else
            {
                Console.WriteLine("Teacher does not exist.");
            };
        }

        //CRUD for courses

        /// <summary>
        /// Add a course to a teacher.
        /// </summary>
        public static void addCourse()
        {
            Console.WriteLine("Enter a course name: ");
            string courseName = Console.ReadLine();

            Teacher teacher = null;
            Menu.includeTeacher();
            if (Validator.yesOrNo())
            {
                Menu.newTeacher();
                if (Validator.yesOrNo())
                {
                    addTeacher();
                }

                listTeachers();
                Console.WriteLine("Select a teacher for this course. ");
                int id = Validator.getId();
                teacher = businessLayer.GetTeacherById(id);

                if(teacher == null)
                {
                    Console.WriteLine("Teacher does not exist.");
                }
                else
                {
                    //create course
                    Course course = new Course()
                    {
                        CourseName = courseName,
                        TeacherId = teacher.TeacherId,
                        EntityState = EntityState.Added
                    };

                    //add course to teacher
                    teacher.EntityState = EntityState.Modified;
                    foreach (Course c in teacher.Courses)
                        c.EntityState = EntityState.Unchanged;
                    teacher.Courses.Add(course);
                    businessLayer.UpdateTeacher(teacher);
                    Console.WriteLine("{0} has been added to the database.", courseName);
                }
            }
            else
            {
                //create course
                Course course = new Course()
                {
                    CourseName = courseName,
                    EntityState = EntityState.Added
                };
                businessLayer.AddCourse(course);
                Console.WriteLine("{0} has been added to the database.", courseName);
            }
        }

        /// <summary>
        /// Update the name of a course.
        /// </summary>
        public static void updateCourse()
        {
            Menu.displaySearchOptions(true);
            int input = Validator.getOptionInput();
            if (input < 3 && input > 0)
                listCourses();
            else if (input == 3)
                listTeachers();

            //find course by name
            if (input == 1)
            {
                Console.WriteLine("Enter a course's name: ");
                Course course = businessLayer.GetCourseByName(Console.ReadLine());
                if (course != null)
                {
                    Console.WriteLine("Change this course's name to: ");
                    course.CourseName = Console.ReadLine();
                    course.EntityState = EntityState.Modified;
                    businessLayer.UpdateCourse(course);
                }
                else
                {
                    Console.WriteLine("Course does not exist.");
                }
            }
            //find course by id
            else if (input == 2)
            {

                int id = Validator.getId();
                Course course = businessLayer.GetCourseById(id);
                if (course != null)
                {
                    Console.WriteLine("Change this course's name to: ");
                    course.CourseName = Console.ReadLine();
                    course.EntityState = EntityState.Modified;
                    businessLayer.UpdateCourse(course);
                }
                else
                {
                    Console.WriteLine("Course does not exist.");
                }
            }
            else if (input == 3)
            {
                int id = Validator.getId();
                Teacher teacher = businessLayer.GetTeacherById(id);
                if (teacher != null)
                {
                    ICollection<Course> courses = teacher.Courses;
                    foreach (Course course in courses) {
                        Console.WriteLine("Course ID: {0}, Name: {1}", course.CourseId, course.CourseName);
                    }

                    if (courses.Count > 0)
                    {
                        int id1 = Validator.getId();
                        Course course1 = businessLayer.GetCourseById(id1);
                        if (course1 != null)
                        {
                            Console.WriteLine("Change this course's name to: ");
                            course1.CourseName = Console.ReadLine();
                            course1.EntityState = EntityState.Modified;
                            businessLayer.UpdateCourse(course1);
                        }
                        else
                        {
                            Console.WriteLine("Course does not exist.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Teacher has no courses.");
                    }
                }
                else
                {
                    Console.WriteLine("Teacher does not exist.");
                }
            }
        }

        /// <summary>
        /// Remove a course in the database.
        /// </summary>
        public static void removeCourse()
        {
            listCourses();
            int id = Validator.getId();
            Course course = businessLayer.GetCourseById(id);
            if (course != null)
            {
                
                Console.WriteLine("{0} has been removed.", course.CourseName);
                course.EntityState = EntityState.Deleted;
                businessLayer.RemoveCourse(course);
            }
            else
            {
                Console.WriteLine("Course does not exist.");
            };
        }


        /// <summary>
        /// List all courses in the database.
        /// </summary>
        public static void listCourses()
        {
            IList<Course> courses = businessLayer.GetAllCourses();
            foreach (Course course in courses)
                Console.WriteLine("Course ID: {0}, Name: {1}", course.CourseId, course.CourseName);
        }
        /// <summary>
        /// Adds a course to an existing teacher
        /// </summary>
        /// <param name="course">Course to be added</param>
        public static void addCourseToTeacher(Course course = null)
        {
            int id;
            // Select a correct course
            if (course == null)
            {
                listCourses();
                id = Validator.getId();
                course = businessLayer.GetCourseById(id);
            }
            // Assign selected course to teacher
            if(course != null)
            {
                listTeachers();
                id = Validator.getId();
                Teacher teacher = businessLayer.GetTeacherById(id);
                if (teacher != null)
                {
                    course.EntityState = EntityState.Modified;
                    course.TeacherId = teacher.TeacherId;
                    businessLayer.UpdateCourse(course);

                    //add course to teacher
                    teacher.EntityState = EntityState.Modified;
                    foreach (Course c in teacher.Courses)
                        c.EntityState = EntityState.Unchanged;
                    teacher.Courses.Add(course);
                    businessLayer.UpdateTeacher(teacher);
                    Console.WriteLine("Course {0} has been added to {1}.", course.CourseId, teacher.TeacherId);
                }
                else
                {
                    Console.WriteLine("Teacher does not exist.");
                }
            }
            else
            {
                Console.WriteLine("Course does not exist.");
            }
        }
        /// <summary>
        /// Moves course from one teacher to another
        /// </summary>
        public static void moveCourse()
        {
            listTeachers();
            // Select a teacher in order to select a course
            int id = Validator.getId();
            Teacher teacher = businessLayer.GetTeacherById(id);
            if (teacher != null)
            {
                ICollection<Course> courses = teacher.Courses;
                // Display the courses that the selected teacher teaches
                foreach (Course course in courses)
                {
                    Console.WriteLine("Course ID: {0}, Name: {1}", course.CourseId, course.CourseName);
                }
                // If the teacher teaches at least one course, allow for a selection of a course
                // Remove the current relation between the selected course and the currently selected teacher
                if (courses.Count > 0)
                {
                    int id1 = Validator.getId();
                    Course course1 = businessLayer.GetCourseById(id1);
                    if (course1 != null)
                    {
                        course1.Teacher = null;
                        course1.TeacherId = null;
                        teacher.Courses.Remove(course1);
                        foreach (Course c in teacher.Courses)
                            c.EntityState = EntityState.Unchanged;
                        teacher.EntityState = EntityState.Modified;
                        course1.EntityState = EntityState.Modified;
                        businessLayer.UpdateCourse(course1);
                        businessLayer.UpdateTeacher(teacher);
                        // Move course to new teacher
                        addCourseToTeacher(course1);
                    }
                    else
                    {
                        Console.WriteLine("Course does not exist.");
                    }
                }
                else
                {
                    Console.WriteLine("Teacher has no courses.");
                }
            }
            else
            {
                Console.WriteLine("Teacher does not exist.");
            }
        }
    }
}
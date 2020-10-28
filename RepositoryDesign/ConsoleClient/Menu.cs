using System;

namespace ConsoleClient
{
    public static class Menu
    {
        /// <summary>
        /// Display all startup options.
        /// </summary>
        public static void displayMenu()
        {
            Console.WriteLine("1.) Create teacher");
            Console.WriteLine("2.) Update teacher");
            Console.WriteLine("3.) Delete teacher");
            Console.WriteLine("4.) list teachers");
            Console.WriteLine("5.) list courses of a teacher");
            Console.WriteLine("6.) Create course");
            Console.WriteLine("7.) Update course");
            Console.WriteLine("8.) Delete course");
            Console.WriteLine("9.) list courses");
            Console.WriteLine("10.) Add course to teacher");
            Console.WriteLine("11.) Move course to another teacher");
            Console.WriteLine("0.) quit");
        }

        /// <summary>
        /// Clear console and print seperator.
        /// </summary>
        public static void clearMenu()
        {
            //Console.Clear();
            Console.WriteLine("\n\n\n--------------------------------------------");
        }

        /// <summary>
        /// Display search options: name or id.
        /// </summary>
        public static void displaySearchOptions(bool updatingCourse = false)
        {
            Console.WriteLine("1.) Find with name");
            Console.WriteLine("2.) Find with ID");
            if (updatingCourse)
            {
                Console.WriteLine("3.) Find by Teacher ID");
            }
        }

        public static void includeTeacher()
        {
            Console.WriteLine("Would you like to give this course a teacher?(y/n)");
        }

        public static void newTeacher()
        {
            Console.WriteLine("Will it be a new teacher?(y/n)");
        }
    }
}

using System;
using ViewModel;
using BL;

namespace TsetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            BLEmployee employee = new BLEmployee();

            foreach (var employees in employee.GetAllEmployees())
            {
                Console.WriteLine("{0} {1}", employees.Id, employees.Name);
            }

            BLProject project = new BLProject();

            foreach (var projects in project.GetAllProjects())
            {
                Console.WriteLine("{0} {1}", projects.Id, projects.Name);
            }


            Console.WriteLine("End.");
            Console.ReadKey();
        }
    }
}

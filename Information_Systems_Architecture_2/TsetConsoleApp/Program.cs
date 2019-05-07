using System;
using ViewModel;
using BL;

namespace TsetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //BLEmployee employee = new BLEmployee();
            //BLProject project = new BLProject();

            //Model.Project p1 = new Model.Project() { Name="dfsdf" };
            //Model.Project p2 = new Model.Project() { Name = "sgsrf" };
            //Model.Employee e1 = new Model.Employee() { Name = "Gegfd", Age = 19, Status = Model.EnumStatus.Employee, Position = Model.EnumPosition.Developer, Salary = 2333};

            //e1.Project.Add(p2);
            //e1.Project.Add(p1);
            //p1.Employees.Add(e1);

            //employee.CreateEmployee(e1);
            //project.CreateProject(p1);
            //project.CreateProject(p2);

            //employee.Commit();

            //foreach (var employees in employee.GetAllEmployees())
            //{
            //    foreach (var i in employees.Project)
            //    {
            //        Console.WriteLine("{0} связанные проекты", i.Name);
            //    }
            //    Console.WriteLine("{0} {1}", employees.Id, employees.Name);
            //}

            //foreach (var projects in project.GetAllProjects())
            //{
            //    Console.WriteLine("{0} {1}", projects.Id, projects.Name);
            //}

            //Console.WriteLine("End EntityRepository.");

            ADOBLEmployee adoEmployee = new ADOBLEmployee();
            ADOBLProject adoProject = new ADOBLProject();

            foreach (var employees in adoEmployee.GetAllEmployees())
            {
                Console.WriteLine("Id: {0} Name: {1} Age: {2} Salary: {3} Status: {4} Position: {5}", employees.Id, employees.Name, employees.Age, employees.Salary, employees.Status, employees.Position);
            }

            var employeeById = adoEmployee.GetByIdEmployee(18);
            Console.WriteLine("GetByIdEmployee Id: {0} Name: {1} Age: {2} Salary: {3} Status: {4} Position: {5}", employeeById.Id, employeeById.Name, employeeById.Age, employeeById.Salary, employeeById.Status, employeeById.Position);

            foreach (var project in adoProject.GetAllProjects())
            {
                Console.WriteLine("{0} {1}", project.Id, project.Name);
            }

            var projectById = adoProject.GetByIdProject(27);
            Console.WriteLine("GetByIdProject Id: {0}, Name: {1}", projectById.Id, projectById.Name);

            Console.WriteLine("End ADORepository.");
            Console.ReadKey();
        }
    }
}

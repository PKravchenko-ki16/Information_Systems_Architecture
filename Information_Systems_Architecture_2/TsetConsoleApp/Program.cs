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
            BLProject project = new BLProject();

            Model.Project p1 = new Model.Project() { Name = "sfdsd" };
            Model.Project p2 = new Model.Project() { Name = "tyuhg" };
            Model.Employee e1 = new Model.Employee() { Name = "Tdfsdf", Age = 39, Status = Model.EnumStatus.Employee, Position = Model.EnumPosition.Developer, Salary = 5675 };

            e1.Project.Add(p2);
            e1.Project.Add(p1);
            p1.Employees.Add(e1);

            //employee.CreateEmployee(e1);
            //project.CreateProject(p1);
            //project.CreateProject(p2);
            //employee.Commit();

            Console.WriteLine("\n GetAllEmployees \n");

            foreach (var employees in employee.GetAllEmployees())
            {
                foreach (var i in employees.Project)
                {
                    Console.Write("{0} ", i.Name);
                }
                Console.WriteLine("связанные проекты c Employee: {0} {1}", employees.Id, employees.Name);
            }

            Console.WriteLine("\n GetByIdEmployee \n");

            var employeeById = employee.GetByIdEmployee(19);
            Console.WriteLine("GetByIdEmployee Id: {0} Name: {1} Age: {2} Salary: {3} Status: {4} Position: {5}", employeeById.Id, employeeById.Name, employeeById.Age, employeeById.Salary, employeeById.Status, employeeById.Position);
            foreach (var i in employeeById.Project)
            {
                Console.WriteLine("Project: {0} {1}", i.Id, i.Name);
            }

            Console.WriteLine("\n GetAllProjects \n");

            foreach (var projects in project.GetAllProjects())
            {
                Console.WriteLine("Project: {0} {1}", projects.Id, projects.Name);
            }

            Console.WriteLine("\n GetByIdProject \n");

            var projectById = project.GetByIdProject(27);
            Console.WriteLine("GetByIdProject Id: {0}, Name: {1}", projectById.Id, projectById.Name);
            foreach (var i in projectById.Employees)
            {
                Console.WriteLine("Employee Id: {0} Name: {1} Age: {2} Salary: {3} Status: {4} Position: {5}", i.Id, i.Name, i.Age, i.Salary, i.Status, i.Position);
            }

            Console.WriteLine("\n End EntityRepository. \n");

            //ADOBLEmployee adoEmployee = new ADOBLEmployee();
            //ADOBLProject adoProject = new ADOBLProject();

            //Console.WriteLine("\n GetAllEmployees \n");

            //foreach (var employees in adoEmployee.GetAllEmployees())
            //{
            //    Console.WriteLine("Id: {0} Name: {1} Age: {2} Salary: {3} Status: {4} Position: {5}", employees.Id, employees.Name, employees.Age, employees.Salary, employees.Status, employees.Position);
            //    foreach (var projects in employees.Project)
            //    {
            //        Console.WriteLine("ProjectId: {0} ProjectName: {1}", projects.Id, projects.Name);
            //    }
            //}

            //Console.WriteLine("\n GetByIdEmployee \n");

            //var employeeById = adoEmployee.GetByIdEmployee(19);
            //Console.WriteLine("GetByIdEmployee Id: {0} Name: {1} Age: {2} Salary: {3} Status: {4} Position: {5}", employeeById.Id, employeeById.Name, employeeById.Age, employeeById.Salary, employeeById.Status, employeeById.Position);
            //foreach (var i in employeeById.Project)
            //{
            //   Console.WriteLine("Project 19: {0} {1}", i.Id, i.Name);
            //}

            //Console.WriteLine("\n GetAllProjects \n");

            //foreach (var project in adoProject.GetAllProjects())
            //{
            //    Console.WriteLine("Project: {0} {1}", project.Id, project.Name);
            //    foreach (var employees in project.Employees)
            //    {
            //        Console.WriteLine("Employees Id: {0} Name: {1} Age: {2} Salary: {3} Status: {4} Position: {5}", employees.Id, employees.Name, employees.Age, employees.Salary, employees.Status, employees.Position);
            //    }
            //}

            //Console.WriteLine("\n GetByIdProject \n");

            //var projectById = adoProject.GetByIdProject(27);
            //Console.WriteLine("GetByIdProject Id: {0}, Name: {1}", projectById.Id, projectById.Name);
            //foreach (var i in projectById.Employees)
            //{
            //    Console.WriteLine("Employee Id: {0} Name: {1} Age: {2} Salary: {3} Status: {4} Position: {5}", i.Id, i.Name, i.Age, i.Salary, i.Status, i.Position);
            //}

            //Console.WriteLine("\n End ADORepository.");
            Console.ReadKey();
        }
    }
}

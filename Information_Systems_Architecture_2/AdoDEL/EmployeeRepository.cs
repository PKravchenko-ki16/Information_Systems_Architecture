using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AdoDEL
{
    internal class EmployeeRepository : AdoRepository<Employee>
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override Employee Get(int id)
        {
            foreach (var emp in Added)
            {
                if (emp.Id == id)
                {
                    return emp;
                }
            }

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string cmdText = string.Concat(
                $"SELECT e.Name as EmployeeName " +
                    $",e.Id as EmployeeId",
                    $",e.Age as EmployeeAge",
                    $",e.Salary as EmployeeSalary",
                    $",e.Status as EmployeeStatus",
                    $",e.Position as EmployeePosition",
                    $",p.Id as ProjectId",
                    $",p.Name as ProjectName ",
                 $"FROM dbo.Employees e ",
                 $"JOIN dbo.Projects p ON p.Id = ANY(SELECT pe.Project_Id FROM dbo.ProjectEmployees pe where pe.Employee_Id = {id}) where e.Id = {id}");

                if (Deleted.Count > 0)
                    cmdText += string.Format(" OR e.Id NOT IN ({0})", string.Join(",", DeletedIds));

                var command = new SqlCommand(cmdText, conn);
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                        var employee = new Employee
                        {
                            Id = (int)reader["EmployeeId"],
                            Name = (string)reader["EmployeeName"],
                            Age = (int)reader["EmployeeAge"],
                            Salary = (decimal)reader["EmployeeSalary"],
                            Status = (Model.EnumStatus)reader["EmployeeStatus"],
                            Position = (Model.EnumPosition)reader["EmployeePosition"],
                            Project = new List<Project>() { new Project { Id = (int)reader["ProjectId"], Name = (string)reader["ProjectName"] } }
                        };
                        return employee;
                }
            }
        }

        public override IEnumerable<Employee> GetAll()
        {
            List<Employee> allEmployee = new List<Employee>();

            foreach (var employee in Added)
            {
                allEmployee.Add(employee);
            }

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string cmdText = string.Concat(
                    "SELECT e.Name as EmployeeName " +
                        ",e.Id as EmployeeId",
                        ",e.Age as EmployeeAge",
                        ",e.Salary as EmployeeSalary",
                        ",e.Status as EmployeeStatus",
                        ",e.Position as EmployeePosition",
                        ",p.Id as ProjectId",
                        ",p.Name as ProjectName ",
                    "FROM dbo.Employees e ",
                    "JOIN dbo.Projects p ON p.Id = ANY(SELECT pe.Project_Id FROM dbo.ProjectEmployees pe where pe.Employee_Id  = e.Id)");

                if (Deleted.Count > 0)
                    cmdText += string.Format(" where e.Id NOT IN ({0})", string.Join(",", DeletedIds));

                var command = new SqlCommand(cmdText, conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var employee = new Employee
                        {
                            Id = (int)reader["EmployeeId"],
                            Name = (string)reader["EmployeeName"],
                            Age = (int)reader["EmployeeAge"],
                            Salary = (decimal)reader["EmployeeSalary"],
                            Status = (Model.EnumStatus)reader["EmployeeStatus"],
                            Position = (Model.EnumPosition)reader["EmployeePosition"],
                        };
                        if (allEmployee.Where(t => t.Id == employee.Id).Count() == 0)
                        {
                            allEmployee.Add(employee);
                        }
                        int selectedemployeesForProject = allEmployee.Where(t => t.Id == employee.Id).Count();
                        if (allEmployee.Where(t => t.Id == employee.Id).Count() != 0)
                        {
                            foreach (var emp in allEmployee)
                            {
                                if (emp.Id == employee.Id) emp.Project.Add(new Project { Id = (int)reader["ProjectId"], Name = (string)reader["ProjectName"] });
                            }
                        }
                    }
                }
            }
            return allEmployee;
        }

        public override string Update()
        {
            string script = string.Empty;
            string delScriptTemplate = "DELETE FROM dbo.Employees WHERE Id IN ({0})";
            string addScriptTemplate = "INSERT INTO [dbo].[Employees] ([Name])VALUES('{0}')";
            if (Deleted.Count > 0)
                script += string.Format(delScriptTemplate, DeletedIds);
            foreach (var employee in Added)
            {
                script += string.Format("\n" + addScriptTemplate, employee.Name);
            }
            return script;
        }
    }
}

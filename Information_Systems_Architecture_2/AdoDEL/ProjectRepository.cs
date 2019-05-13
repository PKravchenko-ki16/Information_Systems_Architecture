using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Model;

namespace AdoDEL
{
    internal class ProjectRepository : AdoRepository<Project>
    {
        private readonly string _connectionString;

        public ProjectRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override Project Get(int id)
        {
            foreach (var project in Added)
            {
                if (project.Id == id)
                {
                   return project;
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
                $"FROM dbo.Projects p ",
                $"JOIN dbo.Employees e ON e.Id = ANY(SELECT pe.Employee_Id FROM dbo.ProjectEmployees pe where pe.Project_Id = {id}) where p.Id = {id}");

                if (Deleted.Count > 0)
                    cmdText += string.Format(" OR Id NOT IN ({0})", string.Join(",", DeletedIds));

                var command = new SqlCommand(cmdText, conn);
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    var project = new Project { Id = (int)reader["ProjectId"], Name = (string)reader["ProjectName"], Employees = new List<Employee>() { new Employee { Id = (int)reader["EmployeeId"],
                            Name = (string)reader["EmployeeName"],
                            Age = (int)reader["EmployeeAge"],
                            Salary = (decimal)reader["EmployeeSalary"],
                            Status = (Model.EnumStatus)reader["EmployeeStatus"],
                            Position = (Model.EnumPosition)reader["EmployeePosition"] } } };
                    return project;
                }
            }
        }

        public override IEnumerable<Project> GetAll()
        {
            List<Project> allProject = new List<Project>();

            foreach (var project in Added)
            {
                allProject.Add(project);
            }

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string cmdText = string.Concat(
                    "SELECT e.Name as EmployeeName " +
                        ",e.Id as EmployeeId",
                        ",e.Age as EmployeeAge",
                        ",e.Status as EmployeeStatus",
                        ",e.Salary as EmployeeSalary",
                        ",e.Position as EmployeePosition",
                        ",p.Id as ProjectId",
                        ",p.Name as ProjectName ",
                    "FROM dbo.Projects p ",
                    "JOIN dbo.Employees e ON e.Id = ANY(SELECT pe.Employee_Id FROM dbo.ProjectEmployees pe where pe.Project_Id  = p.Id)");
            
                if (Deleted.Count > 0)
                    cmdText += string.Format(" where Id NOT IN ({0})", string.Join(",", DeletedIds));

                var command = new SqlCommand(cmdText, conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var project = new Project { Id = (int)reader["ProjectId"], Name = (string)reader["ProjectName"] };

                        if (allProject.Where(t => t.Id == project.Id).Count() == 0)
                        {
                            allProject.Add(project);
                        }
                        int selectedProjectForEmployees = allProject.Where(t => t.Id == project.Id).Count();
                        if (allProject.Where(t => t.Id == project.Id).Count() != 0)
                        {
                            foreach (var proj in allProject)
                            {
                                if (proj.Id == project.Id) proj.Employees.Add(new Employee
                                {
                                    Id = (int)reader["EmployeeId"],
                                    Name = (string)reader["EmployeeName"],
                                    Age = (int)reader["EmployeeAge"],
                                    Salary = (decimal)reader["EmployeeSalary"],
                                    Status = (Model.EnumStatus)reader["EmployeeStatus"],
                                    Position = (Model.EnumPosition)reader["EmployeePosition"],
                                });
                            }
                        }
                    }
                }
            }
            return allProject;
        }

        public override string Update()
        {
            string script = string.Empty;
            string delScriptTemplate = "DELETE FROM dbo.Projects WHERE Id IN ({0})";
            string addScriptTemplate = "INSERT INTO [dbo].[Projects] ([Name])VALUES('{0}')";
            if (Deleted.Count > 0)
                script += string.Format(delScriptTemplate, DeletedIds);
            foreach (var project in Added)
            {
                script += string.Format("\n" + addScriptTemplate, project.Name);
            }
            return script;
        }
    }
}

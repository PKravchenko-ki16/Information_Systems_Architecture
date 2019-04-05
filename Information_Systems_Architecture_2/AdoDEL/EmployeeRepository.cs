using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDEL
{
    internal class EmployeeRepository : AdoRepository<Employee>
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override IEnumerable<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public override string GetUpdateScript()
        {
            throw new NotImplementedException();
        }

        //public override IEnumerable<Employee> GetAll()
        //{
        //    foreach (var employee in Added)
        //    {
        //        yield return employee;
        //    }

        //    using (var conn = new SqlConnection(_connectionString))
        //    {
        //        conn.Open();
        //        string cmdText = string.Concat(
        //            "SELECT p.Name as employeeName " +
        //                ",p.Id as employeeId",
        //                ",c.Id as CityId",
        //                "c.Name as CityName ",
        //            "FROM dbo.People p ",
        //            "JOIN dbo.Cities c ON p.CityId = c.Id");

        //        if (Deleted.Count > 0)
        //            cmdText += string.Format(" where p.Id NOT IN ({0})", string.Join(",", DeletedIds));

        //        var command = new SqlCommand(cmdText, conn);
        //        using (var reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                var employee = new Employee
        //                {
        //                    Id = (int)reader["employeeId"],
        //                    Name = (string)reader["employeeName"],
        //                    ProjectId = (int)reader["ProjectId"],
        //                    Project = new Project { Id = (int)reader["ProjectId"], Name = (string)reader["ProjectName"] }
        //                };
        //                yield return employee;
        //            }
        //        }
        //    }
        //}

        //public override string GetUpdateScript()
        //{
        //    string script = string.Empty;
        //    string delScriptTemplate = "DELETE FROM dbo.People WHERE Id IN ({0})";
        //    string addScriptTemplate = "INSERT INTO [dbo].[People] ([Name],[CityId])VALUES('{0}',{1})";
        //    if (Deleted.Count > 0)
        //        script += string.Format(delScriptTemplate, DeletedIds);
        //    foreach (var employee in Added)
        //    {
        //        script += string.Format("\n" + addScriptTemplate, employee.Name, employee.ProjectId);
        //    }
        //    return script;
        //}
    }
}

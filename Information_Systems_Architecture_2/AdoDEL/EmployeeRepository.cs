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
            foreach (var employee in Added)
            {
                yield return employee;
            }

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string cmdText = string.Concat(
                    "SELECT p.Name as employeeName " +
                        ",p.Id as employeeId",
                    "FROM dbo.Employees p ");

                if (Deleted.Count > 0)
                    cmdText += string.Format(" where p.Id NOT IN ({0})", string.Join(",", DeletedIds));

                var command = new SqlCommand(cmdText, conn);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var employee = new Employee
                        {
                            Id = (int)reader["employeeId"],
                            Name = (string)reader["employeeName"]
                        };
                        yield return employee;
                    }
                }
            }
        }

        public override string GetUpdateScript()
        {
            string script = string.Empty;
            string delScriptTemplate = "DELETE FROM dbo.People WHERE Id IN ({0})";
            string addScriptTemplate = "INSERT INTO [dbo].[People] ([Name])VALUES('{0}')";
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

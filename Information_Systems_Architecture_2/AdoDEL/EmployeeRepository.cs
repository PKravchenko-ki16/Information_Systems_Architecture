using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
            foreach (var employee in Added)
            {
                if (employee.Id == id)
                {
                    return employee;
                }
            }

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string cmdText = $"SELECT Id, Name, Age, Salary, Status, Position FROM dbo.Employees WHERE Id={id}";

                if (Deleted.Count > 0)
                    cmdText += string.Format(" OR e.Id NOT IN ({0})", string.Join(",", DeletedIds));

                var command = new SqlCommand(cmdText, conn);
                using (var reader = command.ExecuteReader())
                {
                  reader.Read();
                  var employee = new Employee
                  {
                     Id = (int)reader["Id"],
                     Name = (string)reader["Name"],
                     Age = (int)reader["Age"],
                     Salary = (decimal)reader["Salary"],
                     Status = (Model.EnumStatus)reader["Status"],
                     Position = (Model.EnumPosition)reader["Position"]
                  };
                  return employee;
                }
            }
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
                string cmdText = "SELECT Id, Name, Age, Salary, Status, Position FROM dbo.Employees";

                if (Deleted.Count > 0)
                    cmdText += string.Format(" where e.Id NOT IN ({0})", string.Join(",", DeletedIds));

                var command = new SqlCommand(cmdText, conn);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var employee = new Employee
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Age = (int)reader["Age"],
                            Salary = (decimal)reader["Salary"],
                            Status = (Model.EnumStatus)reader["Status"],
                            Position = (Model.EnumPosition)reader["Position"]
                        };
                        yield return employee;
                    }
                }
            }
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

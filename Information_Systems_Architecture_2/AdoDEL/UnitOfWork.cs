using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Model;

namespace AdoDEL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;
        private readonly ProjectRepository _projects;
        private readonly EmployeeRepository _employees;

        public UnitOfWork()
        {
            _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DB_ISA;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _employees = new EmployeeRepository(_connectionString);
            _projects = new ProjectRepository(_connectionString);
        }

        public IRepository<Employee> Employees
        {
            get { return _employees; }
        }

        public IRepository<Project> Projects
        {
            get { return _projects; }
        }

        public bool SaveChanges()
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string cmdText = string.Join("\n",
                        new[] { _projects.GetUpdateScript(), _employees.GetUpdateScript() });

                    var command = new SqlCommand(cmdText, conn);
                    command.ExecuteNonQuery();
                }
                Discard();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Discard()
        {
            _projects.Discard();
            _employees.Discard();
        }
    }
}

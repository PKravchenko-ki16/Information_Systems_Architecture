# Information_Systems_Architecture
This project shows DAL (Ado & Entity) & PATTERNS (Unit of Work, Repository) at work

## Comparing ADO.NET and Entity Framework DB Queries

 - Entity Framework
```C#
public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        private readonly DataContext _context;

        public EntityRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }
```
 - ADO.NET

```C#
    internal class EmployeeRepository : AdoRepository<Employee>
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
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
```

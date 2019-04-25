using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDAL
{
    class InitializerEntity
    {
        private List<Employee> _listEmployee = new List<Employee>() { };
        private List<Project> _listProject = new List<Project>() { };

        private static Random random = new Random((int)DateTime.Now.Ticks);

        public List<Employee> GetEntityEmployee()
        {
            RandomEmployee(10);
            return _listEmployee;
        }
        public List<Project> GetEntityProject()
        {
            RandomProject(10);
            return _listProject;
        }

        public void Relationship()
        {
            foreach (var i in _listEmployee)
            {
                i.Project = _listProject.Select(it => it).Where(it => it.Id == 1 & it.Id == 3 & it.Id == 5 & it.Id == 7 & it.Id == 9).ToList();
            }
            foreach (var i in _listProject)
            {
                i.Employee = _listEmployee.Select(it => it).Where(it => it.Id == 0 & it.Id == 2 & it.Id == 3 & it.Id == 4 & it.Id == 6 & it.Id == 8 & it.Id == 10).ToList();

            }
        }
        private void RandomEmployee(int count)
        {
            Random rng = new Random();
            for (var i = 0; i < count; i++)
            {
                Employee employee = new Employee() { };
                employee.Id = i + 1;
                employee.Age = rng.Next(18, 100);
                employee.Salary = rng.Next(700, 5000);
                employee.Name = RandomString(10);
                employee.Position = (EnumPosition)rng.Next(1, 5);
                employee.Status = (EnumStatus)rng.Next(1, 5);
                _listEmployee.Add(employee);
            }

        }
        private void RandomProject(int count)
        {
            Random rng = new Random();
            for (var i = 0; i < count; i++)
            {
                Project project = new Project() { };
                project.Id = i + 1;
                project.Name = RandomString(10);
                _listProject.Add(project);
            }

        }

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}

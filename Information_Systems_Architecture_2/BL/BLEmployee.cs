using System.Collections.Generic;
using EntityDAL;
using Model;

namespace BL
{
    public class BLEmployee
    {
        private UnitOfWork entity;
        public BLEmployee()
        {
          entity = new UnitOfWork();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
           return entity.Employees.GetAll();
        }

        public void CreateEmployee(Employee employee)
        {
            entity.Employees.Create(employee);
        }

        public Employee GetByIdEmployee(int id)
        {
           return entity.Employees.Get(id);
        }

        public void DeleteEmployee(Employee employee)
        {
            entity.Employees.Delete(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            entity.Employees.Update(employee);
        }
    }
}

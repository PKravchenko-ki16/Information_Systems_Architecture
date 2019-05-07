using AdoDEL;
using Model;
using System.Collections.Generic;

namespace BL
{
    public class ADOBLEmployee
    {
        private UnitOfWork entity;
        public ADOBLEmployee()
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

        public void Commit()
        {
           entity.SaveChanges();
        }
    }
}

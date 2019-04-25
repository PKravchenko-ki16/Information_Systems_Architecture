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

        //public void CreateEmployee(Employee employee)
        //{
        //}

        //public Employee GetByIdEmployee(int id)
        //{
        //}

        //public void DeleteEmployee(Employee employee)
        //{
        //}

        //public void UpdateEmployee(Employee employee)
        //{
        //}

        public void Commit()
        {
            entity.SaveChanges();
        }
    }
}

using System;
using Model;

namespace EntityDAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _context = new DataContext();
        private EntityRepository<Employee> _Employees;
        private EntityRepository<Project> _Projects;

        public IRepository<Employee> Employees
        {
            get { return _Employees ?? (_Employees = new EntityRepository<Employee>(_context)); }
        }

        public IRepository<Project> Projects
        {
            get { return _Projects ?? (_Projects = new EntityRepository<Project>(_context)); }
        }

        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Discard()
        {
            _context.Dispose();
            _context = new DataContext();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public interface IUnitOfWork
    {
        IRepository<Employee> Employees { get; }
        IRepository<Project> Projects { get; }
        bool SaveChanges();
        void Discard();
    }
}

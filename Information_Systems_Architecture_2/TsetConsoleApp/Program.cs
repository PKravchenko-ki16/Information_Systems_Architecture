using System;
using Model;
using EntityUnitOfWork = EntityDAL.UnitOfWork;

namespace TsetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var entity = new EntityUnitOfWork();
            for (int i = 0; i < 5; i++)
            {
                entity.Employees.Add(new Employee { Name = string.Format("Employee{0}", i) });
            }
            entity.SaveChanges();
            foreach (var employees in entity.Employees.GetAll())
            {
                Console.WriteLine("{0} ", employees.Id);
            }
            Console.WriteLine("Nenf");
            Console.ReadKey();
        }
    }
}

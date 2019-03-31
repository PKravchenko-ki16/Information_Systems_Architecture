using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDAL
{
    class ContextInitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext db)
        {
            InitializerEntity initializerEntity = new InitializerEntity();

            var employee = initializerEntity.GetEntityEmployee();
            var project = initializerEntity.GetEntityProject();
            initializerEntity.Relationship();

            foreach (var i in employee)
            {
                db.Employees.Add(i);
            }

            foreach (var i in project)
            {
                db.Projects.Add(i);
            };

            db.SaveChanges();
        }
    }
}

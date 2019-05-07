using AdoDEL;
using Model;
using System.Collections.Generic;

namespace BL
{
    public class ADOBLProject
    {
        private UnitOfWork entity;
        public ADOBLProject()
        {
            entity = new UnitOfWork();
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return entity.Projects.GetAll();
        }

        public void CreateProject(Project project)
        {
            entity.Projects.Create(project);
        }

        public Project GetByIdProject(int id)
        {
            return entity.Projects.Get(id);
        }

        public void DeleteProject(Project project)
        {
            entity.Projects.Delete(project);
        }

        public void Commit()
        {
            entity.SaveChanges();
        }
    }
}

using System.Collections.Generic;
using EntityDAL;
using Model;


namespace BL
{
    public class BLProject
    {
        private UnitOfWork entity;
        public BLProject()
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

        public Project GetByIdEmployee(int id)
        {
            return entity.Projects.Get(id);
        }

        public void DeleteEmployee(Project project)
        {
            entity.Projects.Delete(project);
        }

        public void UpdateEmployee(Project project)
        {
            entity.Projects.Update(project);
        }
    }
}

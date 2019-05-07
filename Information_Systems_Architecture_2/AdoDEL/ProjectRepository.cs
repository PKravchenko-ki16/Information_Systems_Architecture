using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace AdoDEL
{
    internal class ProjectRepository : AdoRepository<Project>
    {
        private readonly string _connectionString;

        public ProjectRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override Project Get(int id)
        {
            foreach (var project in Added)
            {
                if (project.Id == id)
                {
                   return project;
                }
            }

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string cmdText = $"SELECT Id, Name FROM dbo.Projects where Id={id}";
                if (Deleted.Count > 0)
                    cmdText += string.Format(" OR Id NOT IN ({0})", string.Join(",", DeletedIds));

                var command = new SqlCommand(cmdText, conn);
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    var project = new Project { Id = (int)reader["Id"], Name = (string)reader["Name"] };
                    return project;
                }
            }
            

        }

        public override IEnumerable<Project> GetAll()
        {
            foreach (var project in Added)
            {
                yield return project;
            }

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string cmdText = "SELECT Id, Name FROM dbo.Projects";
                if (Deleted.Count > 0)
                    cmdText += string.Format(" where Id NOT IN ({0})", string.Join(",", DeletedIds));

                var command = new SqlCommand(cmdText, conn);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var project = new Project { Id = (int)reader["Id"], Name = (string)reader["Name"] };
                        yield return project;
                    }
                }
            }
        }

        public override string Update()
        {
            string script = string.Empty;
            string delScriptTemplate = "DELETE FROM dbo.Projects WHERE Id IN ({0})";
            string addScriptTemplate = "INSERT INTO [dbo].[Projects] ([Name])VALUES('{0}')";
            if (Deleted.Count > 0)
                script += string.Format(delScriptTemplate, DeletedIds);
            foreach (var project in Added)
            {
                script += string.Format("\n" + addScriptTemplate, project.Name);
            }
            return script;
        }
    }
}

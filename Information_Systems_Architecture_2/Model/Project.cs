using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Project : DomainObject
    {
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<Employee> Employee { get; set; }
        public Project()
        {
            Employee = new List<Employee>();
        }
    }
}

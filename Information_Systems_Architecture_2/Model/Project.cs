using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Project : IDomainObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }


        public Project()
        {
            Employees = new List<Employee>();
        }
    }
}

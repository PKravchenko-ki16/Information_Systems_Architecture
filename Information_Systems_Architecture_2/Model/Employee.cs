using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Employee : DomainObject
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        [Column(TypeName = "smallint")]
        public int Status { get; set; }
        [Column(TypeName = "smallint")]
        public int Position { get; set; }
        public ICollection<Project> Project { get; set; }
        public Employee()
        {
            Project = new List<Project>();
        }
    }
}

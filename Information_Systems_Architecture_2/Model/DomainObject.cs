using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public abstract class DomainObject
    {
        [Required]
        [Key]
        int Id { get; }
    }
}

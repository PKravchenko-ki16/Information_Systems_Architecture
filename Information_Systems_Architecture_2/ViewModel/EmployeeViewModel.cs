using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class EmployeeViewModel
    {
            public string Name { get; set; }
            public int Age { get; set; }
            public decimal Salary { get; set; }
            public int Status { get; set; }
            public int Position { get; set; }
            public int? ProjectId { get; set; }
    }
}

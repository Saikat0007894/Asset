using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Models
{
    public class EmployeeModel
    {
        [Required(ErrorMessage = "EmployeeID is required")]
           public  int EmpId { get; set; }
        public string Name { get; set; }
        public  String Depertment  { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Models
{
    public class Departments
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string DepartmentName { get; set; }
        public string DepartmentHead { get; set; }
        public int TotalEmployee { get; set; }
        public List<AppUser> Employees { get; set; }



    }
}

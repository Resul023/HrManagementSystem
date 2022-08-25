using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        public AppUser Employee { get; set; }

    }
}

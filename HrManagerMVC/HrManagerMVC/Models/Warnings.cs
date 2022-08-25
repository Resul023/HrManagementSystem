using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Models
{
    public class Warnings
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string WarningMessage { get; set; }
        public string EmployeeId { get; set; }
        public DateTime WarningTime { get; set; }
        public AppUser Employee { get; set; } 
    }
}

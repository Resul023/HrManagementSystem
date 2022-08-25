using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Models
{
    public class Projects
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string ProjectName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public DateTime IsDoneTime { get; set; }
        
        [Required]
        public bool IsDone { get; set; }
        [Required]
        public bool IsDelete { get; set; }
        [Required]
        [MaxLength(200)]
        public string ProjectDesc { get; set; }
        public List<EmployeeProjects> EmployeeProjects { get; set; } = new List<EmployeeProjects>();
        [NotMapped]
        public List<string> EmployeeIds { get; set; }

    }
}

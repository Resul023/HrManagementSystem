using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Models
{
    public class EmployeeProjects
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public AppUser Employee { get; set; }
        public Projects Projects { get; set; }
    }
}

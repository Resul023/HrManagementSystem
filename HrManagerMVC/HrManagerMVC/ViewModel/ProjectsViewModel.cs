using HrManagerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.ViewModel
{
    public class ProjectsViewModel
    {
        public Projects Project { get; set; }
        public List<Projects> Projects { get; set; }
        public List<AppUser> Employees { get; set; }
        public List<EmployeeProjects> ProjectsEmployees { get; set; }
    }
}

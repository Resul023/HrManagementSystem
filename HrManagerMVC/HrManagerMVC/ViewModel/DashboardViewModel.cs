using HrManagerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.ViewModel
{
    public class DashboardViewModel
    {
        public List<AppUser> Employees { get; set; }
        public List<Todolist> Todolists { get; set; }
        public Todolist Todolist { get; set; }
    }
}

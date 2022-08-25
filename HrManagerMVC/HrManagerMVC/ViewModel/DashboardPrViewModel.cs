using HrManagerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.ViewModel
{
    public class DashboardPrViewModel
    {
        public List<AppUser> Employees { get; set; }
        public List<Projects> Projects { get; set; }
        public Vacation Vacation { get; set; }
        public List<Vacation> Vacations { get; set; }
        public List<Warnings> Warnings { get; set; }
    }
}

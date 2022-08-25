using HrManagerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.ViewModel
{
    public class WarningViewModel
    {
        public string EmployeeId { get; set; }
        public Warnings Warning { get; set; }
    }
}

using HrManagerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.ViewModel
{
    public class AttendanceViewModel
    {
        public List<AppUser> Employeer { get; set; }
        public List<Attendance> Attendances { get; set; }
        public Attendance Attendance { get; set; }
        public List<Holidays> Holidays { get; set; }
        public List<Vacation> Vacations { get; set; }
    }
}

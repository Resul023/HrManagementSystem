using HrManagerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.ViewModel
{
    public class HolidayViewModel
    {
        public Holidays Holiday { get; set; }
        public List<Holidays> HolidayList { get; set; }
    }
}

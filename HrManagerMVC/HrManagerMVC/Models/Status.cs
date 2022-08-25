using HrManagerMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Models
{
    public class Status
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string StatusName { get; set; }
        public List<AppUser> Employee { get; set; }
    }
}

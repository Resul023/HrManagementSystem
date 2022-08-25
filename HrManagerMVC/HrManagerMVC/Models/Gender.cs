using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Models
{
    public class Gender
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Type  { get; set; }
        public List<AppUser> Employee { get; set; }
    }
}

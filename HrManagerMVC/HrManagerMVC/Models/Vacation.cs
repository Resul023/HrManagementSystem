using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Models
{
    public class Vacation
    {
        public int Id { get; set; }
        [Required]
        public DateTime StarDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string VacationAnswer { get; set; }
        [Required]
        [MaxLength(250)]
        public string Text { get; set; }
        public string EmployeeId { get; set; }
        public AppUser Employee { get; set; }
    }
}

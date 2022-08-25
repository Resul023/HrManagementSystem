using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Models
{
    public class Todolist
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(18)]
        public string Title { get; set; }
        [Required]
        [MaxLength(28)]
        public string Desc { get; set; }
        public string EmployeeId { get; set; }
        public AppUser Emloyee { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Models
{
    public class Events
    {
        public int Id { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
        [Required]
        [MaxLength(20)]
        public string EventTitle { get; set; }
        [Required]
        [MaxLength(30)]
        public string EventPlace { get; set; }
        [Required]
        [MaxLength(30)]
        public string EventLocation { get; set; }
    }
}

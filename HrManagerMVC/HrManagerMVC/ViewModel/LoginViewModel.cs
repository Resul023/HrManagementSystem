using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(25)]
        [MinLength(7)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(7)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

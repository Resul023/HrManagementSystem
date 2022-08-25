using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.ViewModel
{
    public class ResetPasswordViewModel
    {
        
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}

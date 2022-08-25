using HrManagerMVC.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.ViewModel
{
    public class CreateEmployeeViewModel
    {
        [Required]
        [MaxLength(30)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(7)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(7)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(8)]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(9)]
        public string Phone { get; set; }
        [Required]
        public double Salary { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(7)]
        public string FinNo { get; set; }
        [Required]
        [MaxLength(9)]
        public string SeriaNo { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int DepartmentId { get; set; }
        public Departments Department { get; set; }

        public string Image { get; set; }
        public bool IsDepartmentHead { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }

}

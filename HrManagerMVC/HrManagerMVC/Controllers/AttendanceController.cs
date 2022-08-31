using HrManagerMVC.DAL;
using HrManagerMVC.Models;
using HrManagerMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Controllers
{
    [Authorize(Roles = "Hr")]
    public class AttendanceController : Controller
    {
        private readonly AppDbContext _context;

        public AttendanceController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "attendance";
            
            AttendanceViewModel attendance = new AttendanceViewModel
            {
                Attendances = _context.Attendances.Include(x => x.Employee).Where(x => x.Employee.IsQuitted == false ).ToList(),
                Employeer = _context.Users.Include(x => x.Department).Include(x => x.Status).Where(x => x.IsQuitted == false).ToList(),
                Holidays = _context.Holidays.ToList(),
                Vacations = _context.Vacations.ToList(),
            };

            return View(attendance);
        }
        public IActionResult Create()
        {
            AttendanceViewModel attendanceVW = new AttendanceViewModel
            {
                Employeer = _context.Users.Include(x => x.Department).Include(x => x.Status).Where(x => x.IsQuitted == false).ToList(),
            };
            return View(attendanceVW);
        }
        [HttpPost]
        public IActionResult Create(AttendanceViewModel attendanceVW)
        {
            foreach (var item in attendanceVW.Employeer)
            {
                if (!_context.Users.Any(x => x.Id == item.Id))
                {
                    ModelState.AddModelError("", "This employee is not exists");
                }
            }
            foreach (var attendances in attendanceVW.Attendances)
            {
                attendances.DateTime = DateTime.UtcNow.Date;
                foreach (var holiday in _context.Holidays)
                {
                    if (!(holiday.StartDate > attendances.DateTime || holiday.EndDate < attendances.DateTime))
                    {
                        ModelState.AddModelError("", "There are holiday on this day");
                    }
                }
                foreach (var vacation in _context.Vacations)
                {
                    if (vacation.EmployeeId == attendances.EmployeeId && vacation.VacationAnswer == "False")
                    {
                        attendances.IsActive = true;
                    }
                }
                if (!_context.Attendances.Any(x => x.DateTime.Day == attendances.DateTime.Day && x.DateTime.Month == attendances.DateTime.Month && x.DateTime.Year == attendances.DateTime.Year))
                {
                    _context.Attendances.Add(attendances);
                }
                else
                {
                    ModelState.AddModelError("", "You took attendance today");
                }
            }
            if (!ModelState.IsValid)
            {
                AttendanceViewModel attendance = new AttendanceViewModel
                {
                    Employeer = _context.Users.Include(x => x.Department).Include(x => x.Status).Where(x => x.IsQuitted == false).ToList(),
                };
                return View(attendance);
            }

            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}

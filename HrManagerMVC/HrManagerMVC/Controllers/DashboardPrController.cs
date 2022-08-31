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
    [Authorize(Roles = "Hr,Employee")]
    public class DashboardPrController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardPrController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "dashboardpr";
            DashboardPrViewModel dashboardprVW = new DashboardPrViewModel
            {
                Employees = _context.Users.Where(x => x.IsQuitted == false).Include(x => x.Gender).Include(x => x.Warnings).Include(x => x.Status).Include(x => x.Department).Include(x => x.EmployeeProjects).ToList(),
                Projects = _context.Projects.Include(x => x.EmployeeProjects).ToList(),
                Vacations = _context.Vacations.ToList(),
                Warnings = _context.Warnings.ToList(),
            };
            return View(dashboardprVW);
        }
        [HttpPost]
        public IActionResult Index(Vacation vacation)
        {

            if (vacation.StarDate >= vacation.EndDate)
            {
                ModelState.AddModelError("", "Start date must be less than End date");
            }
            if ((vacation.StarDate < DateTime.UtcNow || vacation.EndDate < DateTime.UtcNow))
            {
                ModelState.AddModelError("", "Check your date again");
            }
            foreach (var holiday in _context.Holidays)
            {
                if (vacation.StarDate < holiday.EndDate || vacation.EndDate < holiday.StartDate)
                {
                    ModelState.AddModelError("", "Vacation time coincides with holiday time");
                }
            }
            if (!_context.Users.Any(x => x.Id == vacation.EmployeeId))
            {
                ModelState.AddModelError("", "There are problem check it again");
            }
            CheckVacation(vacation);
            if (!ModelState.IsValid)
            {
                DashboardPrViewModel dashboardprVW = new DashboardPrViewModel
                {
                    Employees = _context.Users.Where(x => x.IsQuitted == false).Include(x => x.Gender).Include(x => x.Warnings).Include(x => x.Status).Include(x => x.Department).Include(x => x.EmployeeProjects).ToList(),
                    Projects = _context.Projects.Include(x => x.EmployeeProjects).ToList(),
                    Vacations = _context.Vacations.ToList(),
                    Warnings = _context.Warnings.ToList(),
                };
                return View(dashboardprVW);
            }
            vacation.VacationAnswer = "Null";
            _context.Vacations.Add(vacation);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public void CheckVacation(Vacation vacation)
        {
            
            if (_context.Vacations.Any(x=>x.EmployeeId == vacation.EmployeeId))
            {
                foreach (var vacationDb in _context.Vacations)
                {
                    if (vacation.EmployeeId == vacationDb.EmployeeId)
                    {
                        if ((vacation.StarDate < vacationDb.EndDate) || (vacation.EndDate > vacationDb.StarDate))
                        {
                            ModelState.AddModelError("", "Your vacation's times coincide");
                            break;
                        }
                    }
                }
            }
        }
    }
}

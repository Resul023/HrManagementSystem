using HrManagerMVC.DAL;
using HrManagerMVC.Models;
using HrManagerMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "projects";
            ProjectsViewModel projectVW = new ProjectsViewModel
            {
                Employees = _context.Users.Include(x => x.Status).Include(x => x.Department).Include(x => x.EmployeeProjects).Where(x => x.IsQuitted == false).ToList(),
                Projects = _context.Projects.Where(x =>x.IsDelete == false ).Include(x => x.EmployeeProjects).ToList(),

            };

            return View(projectVW);
        }
        public IActionResult Create()
        {
            ViewBag.Employee = _context.Users.Where(x => x.IsQuitted == false).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Projects project)
        {
            CheckEmployee(project);

            foreach (var empId in project.EmployeeIds)
            {
                EmployeeProjects empProject = new EmployeeProjects
                {
                    Projects = project,
                    EmployeeId = empId
                };
                project.EmployeeProjects.Add(empProject);
            }
            if (project.StartDate >= project.EndDate)
            {
                ModelState.AddModelError("", "Start Date must be less than End Date");
            }
            if (project.StartDate < DateTime.UtcNow)
            {
                ModelState.AddModelError("", "Check you time again");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Employee = _context.Users.Where(x => x.IsQuitted == false).ToList();
                return View();
            }
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Edit(int Id)
        {
            var isExists = _context.Projects.Where(x => x.IsDone == false).Include(x => x.EmployeeProjects).FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            ViewBag.Employee = _context.Users.Where(x => x.IsQuitted == false).ToList();
            isExists.EmployeeIds = isExists.EmployeeProjects.Select(x => x.EmployeeId).ToList();
            return View(isExists);
        }
        [HttpPost]
        public IActionResult Edit(Projects project)
        {
            var isExists = _context.Projects.Where(x => x.IsDone == false).Include(x => x.EmployeeProjects).FirstOrDefault(x => x.Id == project.Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            isExists.EmployeeProjects.RemoveAll(empId => !project.EmployeeIds.Any(x => x == empId.EmployeeId));

            if (project.EmployeeIds != null)
            {
                foreach (var empId in project.EmployeeIds.Where(x => !isExists.EmployeeProjects.Any(empId => empId.EmployeeId == x)))
                {
                    EmployeeProjects empProject = new EmployeeProjects
                    {
                        Projects = project,
                        EmployeeId = empId
                    };
                    isExists.EmployeeProjects.Add(empProject);
                }
            }


            if (project.StartDate >= project.EndDate)
            {
                ModelState.AddModelError("", "Start Date must be less than End Date");
            }
            if (project.StartDate < DateTime.UtcNow)
            {
                ModelState.AddModelError("", "Check you time again");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Employee = _context.Users.Where(x => x.IsQuitted == false).ToList();
                return View();
            }
            isExists.ProjectDesc = project.ProjectDesc;
            isExists.ProjectName = project.ProjectName;
            isExists.StartDate = project.StartDate;
            isExists.EndDate = project.EndDate;
            isExists.IsDone = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int Id)
        {
            var isExists = _context.Projects.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            isExists.IsDelete = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        private void CheckEmployee(Projects project)
        {
            if (project.EmployeeIds != null)
            {
                foreach (var empId in project.EmployeeIds)
                {
                    if (!_context.Users.Any(x => x.Id == empId))
                    {
                        ModelState.AddModelError("EmployeeIds", "Employee is not exists");
                        return;
                    }
                }
            }
        }
    }
}

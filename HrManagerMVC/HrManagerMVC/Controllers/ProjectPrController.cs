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
    public class ProjectPrController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectPrController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "projectspr";
            ProjectsViewModel projectVW = new ProjectsViewModel
            {
                Employees = _context.Users.Include(x => x.Status).Include(x => x.Department).Include(x => x.EmployeeProjects).Where(x => x.IsQuitted == false).ToList(),
                Projects = _context.Projects.Where(x => x.IsDone == false && x.IsDelete == false).Include(x => x.EmployeeProjects).ToList(),

            };
            return View(projectVW);
        }
        public IActionResult Answeryes(Projects projects)
        {
            var isExist = _context.Projects.FirstOrDefault(x => x.Id == projects.Id);
            if (isExist == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            isExist.IsDoneTime = DateTime.UtcNow;
            isExist.IsDone = true;
            _context.SaveChanges();
            return RedirectToAction("index", "ProjectPr");
        }
    }
}

using HrManagerMVC.DAL;
using HrManagerMVC.Models;
using HrManagerMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Controllers
{
    //[Authorize]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(AppDbContext context,UserManager<AppUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "home";
            DashboardViewModel dashboardVW = new DashboardViewModel
            {
                Employees = _context.Users.Where(x => x.IsQuitted == false).Include(x => x.Department).Include(x=>x.EmployeeProjects).Include(x => x.Status).ToList(),
                Todolists = _context.Todolists.ToList(),
            };
            return View(dashboardVW);
        }
        
        public IActionResult Error()
        {
            return View();
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Todolist list)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            list.EmployeeId = user.Id;
            if (!_context.Users.Any(x => x.Id == list.EmployeeId))
            {
                ModelState.AddModelError("", "This employee is not exists");
                return View();
            }
            _context.Todolists.Add(list);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Delete(int Id)
        {
            var isExists = _context.Todolists.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            _context.Remove(isExists);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}

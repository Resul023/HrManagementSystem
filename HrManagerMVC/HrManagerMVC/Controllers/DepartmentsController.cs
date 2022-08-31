using HrManagerMVC.DAL;
using HrManagerMVC.Models;
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
    [Authorize(Roles = "Hr")]

    public class DepartmentsController : Controller
    {
        private readonly AppDbContext _context;
        public DepartmentsController(AppDbContext context )
        {
            this._context = context;

        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "departments";
            List<Departments> departments= _context.Departments.ToList();
            foreach (var item in departments)
            {
                item.TotalEmployee = _context.Users.Where(x =>x.IsQuitted==false && x.DepartmentId == item.Id).Count();
            }
            if (_context.Users.Where(x => x.IsQuitted == false && x.IsDepartmentHead == true).Count()>0)
            {
                ViewData["employee"] = _context.Users.Where(x => x.IsQuitted == false && x.IsDepartmentHead == true).ToList();
            }
            return View(departments);
        }
        public IActionResult Create()
        {
            ViewBag.Employee = _context.Users.Where(x => x.IsQuitted == false).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Departments department)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Employee = _context.Users.Where(x => x.IsQuitted == false).ToList();
                return View();
            }
            
            _context.Departments.Add(department);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int Id)
        {
            Departments isExists = _context.Departments.FirstOrDefault(x => x.Id == Id);
            ViewBag.Employee = _context.Users.Where(x => x.IsQuitted == false).ToList();
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(isExists);
        }
        [HttpPost]
        public IActionResult Edit(Departments department)
        {
            Departments isExists = _context.Departments.FirstOrDefault(x => x.Id == department.Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            /*if (!_context.Users.Any(x => x.Id == department.EmployeeId))
            {
                ModelState.AddModelError("EmployeeId", "This employee is not exists");
            }*/
            if (!ModelState.IsValid)
            {
                ViewBag.Employee = _context.Users.Where(x => x.IsQuitted == false).ToList();
                return View();
            }
            isExists.DepartmentName = department.DepartmentName;
            /*isExists.EmployeeId = department.EmployeeId;*/
            isExists.TotalEmployee = _context.Users.Count();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int Id)
        {
            var isExists = _context.Departments.FirstOrDefault(x => x.Id == Id);
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

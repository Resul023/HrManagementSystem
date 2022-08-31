using HrManagerMVC.DAL;
using HrManagerMVC.Helper;
using HrManagerMVC.Models;
using HrManagerMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;

        public EmployeesController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment env)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._env = env;
            this._context = context;
        }
        /*public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole("Hr"));
            await _roleManager.CreateAsync(new IdentityRole("Employee"));
            return Ok();
        }*/
        public IActionResult Index()
        {
            ViewBag.ActivePage = "employees";
            List<AppUser> users = _userManager.Users.Include(x => x.Status).Include(x => x.Gender).Include(x => x.Department).Where(x => x.IsQuitted == false).ToList();
            ViewData["department"] = _context.Departments.ToList();
            ViewData["status"] = _context.Status.ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            if (_context.Status.Count() > 0 && _context.Departments.Count() > 0)
            {
                ViewBag.Status = _context.Status.ToList();
                ViewBag.Gender = _context.Genders.ToList();
                ViewBag.Department = _context.Departments.ToList();
                return View();
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeViewModel employeeVW)
        {
            if (employeeVW.ImageFile != null)
            {
                CheckImageFileForModel(employeeVW);
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Image is required!!!");
            }
            if (!_context.Genders.Any(x => x.Id == employeeVW.GenderId))
            {
                ModelState.AddModelError("GenderId", "This gender is not exists");
            }
            if (!_context.Status.Any(x => x.Id == employeeVW.StatusId))
            {
                ModelState.AddModelError("StatusId", "This status is not exists");
            }
            if (_context.Users.Any(x => x.IsQuitted == false && x.IsDepartmentHead == true && x.DepartmentId == employeeVW.DepartmentId && employeeVW.IsDepartmentHead == true))
            {
                ModelState.AddModelError("IsDepartmentHead", "This department has a head ");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Status = _context.Status.ToList();
                ViewBag.Gender = _context.Genders.ToList();
                ViewBag.Department = _context.Departments.ToList();
                return View();
            }
            employeeVW.Image = FileManager.Save(_env.WebRootPath, "upload/employee", employeeVW.ImageFile);
            AppUser employer = new AppUser
            {
                UserName = employeeVW.UserName,
                FullName = employeeVW.FullName,
                Email = employeeVW.Email,
                PhoneNumber = employeeVW.Phone,
                Salary = employeeVW.Salary,
                BirthDate = employeeVW.BirthDate,
                FinNo = employeeVW.FinNo,
                SeriaNo = employeeVW.SeriaNo,
                GenderId = employeeVW.GenderId,
                StatusId = employeeVW.StatusId,
                DepartmentId = employeeVW.DepartmentId,
                IsDepartmentHead = employeeVW.IsDepartmentHead,
                Image = employeeVW.Image,
                JoinDate = DateTime.UtcNow.Date,
                IsQuitted = false,
            };

            var result = await _userManager.CreateAsync(employer, employeeVW.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                ViewBag.Status = _context.Status.ToList();
                ViewBag.Gender = _context.Genders.ToList();
                ViewBag.Department = _context.Departments.ToList();
                return View();
            }
            await _userManager.AddToRoleAsync(employer, "Employee");
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Edit(string Id)
        {
            if (_context.Status.Count() > 0 && _context.Departments.Count() > 0)
            {
                var isExists = await _userManager.FindByIdAsync(Id);
                if (isExists == null)
                {
                    return RedirectToAction("error", "dashboard");
                }
                ViewBag.Status = _context.Status.ToList();
                ViewBag.Gender = _context.Genders.ToList();
                ViewBag.Department = _context.Departments.ToList();
                return View(isExists);
            }
            return RedirectToAction("index");

        }
        [HttpPost]
        public async Task<IActionResult> Edit(AppUser employee)
        {
            AppUser isExists = await _userManager.FindByIdAsync(employee.Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            if (employee.ImageFile != null)
            {
                CheckImageFileForEmployee(employee);
                string newFileName = FileManager.Save(_env.WebRootPath, "upload/employee", employee.ImageFile);
                FileManager.Delete(_env.WebRootPath, "upload/employee", isExists.Image);
                isExists.Image = newFileName;
            }
            if (employee.DepartmentId != isExists.DepartmentId)
            {
                CheckOtherDepartmentHead(employee);
            }
            if (!_context.Genders.Any(x => x.Id == employee.GenderId))
            {
                ModelState.AddModelError("GenderId", "This gender is not exists");
            }
            if (!_context.Status.Any(x => x.Id == employee.StatusId))
            {
                ModelState.AddModelError("StatusId", "This status is not exists");
            }
            if (_context.Users.Any(x => x.IsQuitted == false && x.IsDepartmentHead == true && x.DepartmentId == employee.DepartmentId && employee.Id !=isExists.Id))
            {
                ModelState.AddModelError("IsDepartmentHead", "This department has a head ");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Status = _context.Status.ToList();
                ViewBag.Gender = _context.Genders.ToList();
                ViewBag.Department = _context.Departments.ToList();
                return View();
            }
            isExists.UserName = employee.UserName;
            isExists.FullName = employee.FullName;
            isExists.Email = employee.Email;
            isExists.PhoneNumber = employee.PhoneNumber;
            isExists.Salary = employee.Salary;
            isExists.BirthDate = employee.BirthDate;
            isExists.FinNo = employee.FinNo;
            isExists.SeriaNo = employee.SeriaNo;
            isExists.GenderId = employee.GenderId;
            isExists.StatusId = employee.StatusId;
            isExists.DepartmentId = employee.DepartmentId;
            isExists.IsDepartmentHead = employee.IsDepartmentHead;
            isExists.IsQuitted = false;
            await _userManager.UpdateAsync(isExists);
            return RedirectToAction("index");

        }
        public IActionResult Warnings(string Id)
        {
            WarningViewModel warning = new WarningViewModel
            {
                EmployeeId = Id,
            };
            return View(warning);
        }
        [HttpPost]
        public async Task<IActionResult> WarningsCreate(WarningViewModel warningVW)
        {
            AppUser isExists = await _userManager.FindByIdAsync(warningVW.Warning.EmployeeId);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            warningVW.Warning.WarningTime = DateTime.UtcNow;
            _context.Warnings.Add(warningVW.Warning);
            _context.SaveChanges();
            return RedirectToAction("index");

        }


        public async Task<IActionResult> Delete(string Id)
        {
            var isExists = await _userManager.FindByIdAsync(Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            isExists.IsQuitted = true;
            isExists.IsQuitedDate = DateTime.UtcNow.Date;
            await _userManager.UpdateAsync(isExists);
            return RedirectToAction("index");
        }

        //Create Hr Side
        public IActionResult CreateHr()
        {
            if (_context.Status.Count() > 0 && _context.Departments.Count() > 0)
            {
                ViewBag.Status = _context.Status.ToList();
                ViewBag.Gender = _context.Genders.ToList();
                ViewBag.Department = _context.Departments.ToList();
                return View();
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateHr(CreateEmployeeViewModel employeeVW)
        {
            if (employeeVW.ImageFile != null)
            {
                CheckImageFileForModel(employeeVW);
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Image is required!!!");
            }
            if (!_context.Genders.Any(x => x.Id == employeeVW.GenderId))
            {
                ModelState.AddModelError("GenderId", "This gender is not exists");
            }
            if (!_context.Status.Any(x => x.Id == employeeVW.StatusId))
            {
                ModelState.AddModelError("StatusId", "This status is not exists");
            }
            if (_context.Users.Any(x => x.IsQuitted == false && x.IsDepartmentHead == true && x.DepartmentId == employeeVW.DepartmentId && employeeVW.IsDepartmentHead == true))
            {
                ModelState.AddModelError("IsDepartmentHead", "This department has a head ");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Status = _context.Status.ToList();
                ViewBag.Gender = _context.Genders.ToList();
                ViewBag.Department = _context.Departments.ToList();
                return View();
            }
            employeeVW.Image = FileManager.Save(_env.WebRootPath, "upload/employee", employeeVW.ImageFile);
            AppUser employer = new AppUser
            {
                UserName = employeeVW.UserName,
                FullName = employeeVW.FullName,
                Email = employeeVW.Email,
                PhoneNumber = employeeVW.Phone,
                Salary = employeeVW.Salary,
                BirthDate = employeeVW.BirthDate,
                FinNo = employeeVW.FinNo,
                SeriaNo = employeeVW.SeriaNo,
                GenderId = employeeVW.GenderId,
                StatusId = employeeVW.StatusId,
                DepartmentId = employeeVW.DepartmentId,
                IsDepartmentHead = employeeVW.IsDepartmentHead,
                Image = employeeVW.Image,
                JoinDate = DateTime.UtcNow.Date,
                IsQuitted = false,
            };

            var result = await _userManager.CreateAsync(employer, employeeVW.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                ViewBag.Status = _context.Status.ToList();
                ViewBag.Gender = _context.Genders.ToList();
                ViewBag.Department = _context.Departments.ToList();
                return View();
            }
            await _userManager.AddToRoleAsync(employer, "Hr");
            return RedirectToAction("index");
        }
 


        private void CheckImageFileForModel(CreateEmployeeViewModel employee)
        {
            if (employee.ImageFile.ContentType != "image/png" && employee.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFiles", "File format must be image/png or image/jpeg");
            }
            if (employee.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFiles", "File size must be less than 2MB");
            }

        }
        private void CheckImageFileForEmployee(AppUser employee)
        {
            if (employee.ImageFile.ContentType != "image/png" && employee.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFiles", "File format must be image/png or image/jpeg");
            }
            if (employee.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFiles", "File size must be less than 2MB");
            }

        }
        private void CheckOtherDepartmentHead(AppUser employee)
        {
            foreach (var item in _context.Users)
            {
                if (item.DepartmentId == employee.DepartmentId && item.IsDepartmentHead == true && employee.IsDepartmentHead == true)
                {
                    ModelState.AddModelError("IsDepartmentHead", "This department have a head");
                }
            }
        }

    }
}

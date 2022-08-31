using HrManagerMVC.DAL;
using HrManagerMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Controllers
{
    [Authorize(Roles = "Hr")]
    public class StatusController : Controller
    {
        private readonly AppDbContext _context;

        public StatusController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "status";
            var status = _context.Status.ToList();
            ViewData["employee"] = _context.Users.Where(x => x.IsQuitted == false && x.IsDepartmentHead == true).ToList();
            return View(status);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Status status)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Status.Add(status);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int Id)
        {
            var isExists = _context.Status.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(isExists);
        }

        [HttpPost]
        public IActionResult Edit(Status status)
        {
            var isExists = _context.Status.FirstOrDefault(x => x.Id == status.Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            isExists.StatusName = status.StatusName;

            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int Id)
        {
            var isExists = _context.Status.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            _context.Status.Remove(isExists);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}

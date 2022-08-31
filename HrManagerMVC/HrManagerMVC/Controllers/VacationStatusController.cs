using HrManagerMVC.DAL;
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
    public class VacationStatusController : Controller
    {
        private readonly AppDbContext _context;

        public VacationStatusController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var vacations = _context.Vacations.Include(x=>x.Employee).ToList();
            ViewBag.ActivePage = "vacationstatus";
            return View(vacations);
        }
        public IActionResult AnswerYes(int Id)
        {
            var isExists = _context.Vacations.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("error","dashboard");
            }
            isExists.VacationAnswer = "True";
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult AnswerNo(int Id)
        {
            var isExists = _context.Vacations.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            isExists.VacationAnswer = "False";
            _context.SaveChanges();
            return RedirectToAction("index");
        }


    }
}

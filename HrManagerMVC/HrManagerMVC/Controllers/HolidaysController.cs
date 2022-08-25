using HrManagerMVC.DAL;
using HrManagerMVC.Models;
using HrManagerMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HrManagerMVC.Controllers
{
    public class HolidaysController : Controller
    {
        private readonly AppDbContext _context;

        public HolidaysController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "holidays";
            HolidayViewModel holiday = new HolidayViewModel
            {
                HolidayList = _context.Holidays.ToList(),
            };
            
            return View(holiday);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Holidays holiday)
        {
            
            if (holiday.StartDate >= holiday.EndDate)
            {
                ModelState.AddModelError("StartDate", "Start date must be small than End date ");
            }
            foreach (var item in _context.Holidays)
            {
                if ((holiday.StartDate < item.StartDate && holiday.EndDate < item.StartDate) || (holiday.StartDate > item.EndDate))
                {
                    _context.Add(holiday);
                }
                else
                {
                    ModelState.AddModelError("StartDate", "Your holiday is falling in a row with other holidays");
                }
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int Id)
        {
            var isExists = _context.Holidays.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(isExists);
        }

        [HttpPost]
        public IActionResult Edit(Holidays holiday)
        {
            var isExists = _context.Holidays.FirstOrDefault(x => x.Id == holiday.Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            if(holiday.StartDate >= holiday.EndDate)
            {
                ModelState.AddModelError("StartDate", "Start date must be small than End date ");
            }
            foreach (var item in _context.Holidays)
            {
                isExists.StartDate = new DateTime(1,1, 0001);
                isExists.EndDate = new DateTime(1, 1, 0001);
                if ((holiday.StartDate < item.StartDate && holiday.EndDate < item.StartDate) || (holiday.StartDate > item.EndDate))
                {
                    isExists.StartDate = holiday.StartDate;
                    isExists.EndDate = holiday.EndDate;
                    isExists.Name = holiday.Name;
                }
                else
                {
                    ModelState.AddModelError("StartDate", "Your holiday is falling in a row with other holidays");
                }
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int Id)
        {
            var isExists = _context.Holidays.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            _context.Holidays.Remove(isExists);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}

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
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "events";
            var events = _context.Events.ToList();
            ViewData["events"] = _context.Events.ToList();
            return View(events);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Events events)
        {
            string startDate = events.StartDate;
            DateTime startDateTime = DateTime.Parse(startDate);
            string endDate = events.EndDate;
            DateTime endDateTime = DateTime.Parse(endDate);
            if (startDateTime >= endDateTime)
            {
                ModelState.AddModelError("StartDate", "Start date must be small than End date ");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Add(events);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int Id)
        {
            var isExists = _context.Events.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(isExists);
        }

        [HttpPost]
        public IActionResult Edit(Events events)
        {
            var isExists = _context.Events.FirstOrDefault(x => x.Id == events.Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            DateTime startDateTime = DateTime.Parse(events.StartDate);
            DateTime endDateTime = DateTime.Parse(events.EndDate);
            if (startDateTime >= endDateTime)
            {
                ModelState.AddModelError("StartDate", "Start date must be small than End date ");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            isExists.StartDate = events.StartDate;
            isExists.EndDate = events.EndDate;
            isExists.EventLocation = events.EventLocation;
            isExists.EventPlace = events.EventPlace;
            isExists.EventTitle = events.EventTitle;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int Id)
        {
            var isExists = _context.Events.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            _context.Events.Remove(isExists);
            _context.SaveChanges();
            return RedirectToAction("index");
        }


    }
}

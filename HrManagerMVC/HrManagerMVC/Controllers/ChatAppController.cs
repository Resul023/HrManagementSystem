using HrManagerMVC.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Controllers
{
    public class ChatAppController : Controller
    {
        private readonly AppDbContext _context;

        public ChatAppController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var employees = _context.Users.Where(x => x.IsQuitted == false).ToList();
            return View(employees);
        }
    }
}

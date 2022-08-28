using HrManagerMVC.DAL;
using HrManagerMVC.Models;
using HrManagerMVC.ViewModel;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public ChatAppController(AppDbContext context,UserManager<AppUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "chatapp";
            var employees = _context.Users.Where(x => x.IsQuitted == false).ToList();
            return View(employees);
        }
        public async Task<IActionResult> SendMessage([FromBody] SendMessage message)
        {
            if (message == null || string.IsNullOrWhiteSpace(message.Text))
                return BadRequest();
            var fromUser = await _userManager.FindByNameAsync(User.Identity.Name);
            message.FromUserId = fromUser.Id;
            message.createdAt = DateTime.UtcNow;
            _context.Messages.Add(message);
            _context.SaveChanges();
            return Ok();
        }
        public async Task<IActionResult> GetData([FromBody] string id)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var messageData = _context.Messages.Where(x => (x.ToUserId == currentUser.Id && x.FromUserId == id) || (x.FromUserId == currentUser.Id && x.ToUserId == id)).ToList();
            return Ok(messageData);
        }

    }
}

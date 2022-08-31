using HrManagerMVC.Models;
using HrManagerMVC.Utils;
using HrManagerMVC.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace HrManagerMVC.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser employee = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName && x.IsQuitted == false);
            if (employee == null)
            {
                ModelState.AddModelError("", "UserName or Password is not correct!!");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(employee, user.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is not correct!!");
                return View();
            }
            var role = await _userManager.GetRolesAsync(employee);
            if (role[0].ToString().ToUpper() == "HR")
            {
                return RedirectToAction("index", "dashboard");
            }
            return RedirectToAction("index", "DashboardPr");

        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "login");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest();

            var isExists = await _userManager.Users.FirstOrDefaultAsync(x=>x.IsQuitted == false && x.Email == email);
            if (isExists == null)
                return RedirectToAction("error", "dashboard");

            var token = await _userManager.GeneratePasswordResetTokenAsync(isExists);

            var link = Url.Action("ResetPassword", "Login", new { isExists.Id, token }, protocol: HttpContext.Request.Scheme);
            var message = $"<a href='{link}'>Click here</a>";

            await EmailUtil.SendEmailAsync(email, "Reset Password", message);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> ResetPassword(string id, string token)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(token))
                return BadRequest();

            var isExists = await _userManager.Users.FirstOrDefaultAsync(x => x.IsQuitted == false && x.Id == id);

            if (isExists == null)
                return RedirectToAction("error", "dashboard");

            ResetPasswordViewModel resetPasswordVW = new ResetPasswordViewModel
            {
                Email = isExists.Email,
                Token = token
            };

            return View(resetPasswordVW);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string id,ResetPasswordViewModel resetPasswordVW)
        {

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(resetPasswordVW.Token))
                return BadRequest();
            if (string.IsNullOrEmpty(resetPasswordVW.NewPassword) || string.IsNullOrEmpty(resetPasswordVW.ConfirmPassword))
            {
                ModelState.AddModelError("", "New password and Confirm password is required");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            var isExists = await _userManager.Users.FirstOrDefaultAsync(x => x.IsQuitted == false && x.Id == id);
            if (isExists == null)
                return RedirectToAction("error", "dashboard");
            var result = await _userManager.ResetPasswordAsync(isExists, resetPasswordVW.Token, resetPasswordVW.NewPassword);
            if (result.Errors == null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                    return View();
            }

            return RedirectToAction("index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotiCv.AppCode.Extension;
using MotiCv.Models.FormModels;
using Turbo.WebUI.Models.DbContexts;
using Turbo.WebUI.Models.Entities.Membership;

namespace Turbo.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]


    public class DashboardController : Controller
    {
        readonly TurboDbContext db;
        readonly SignInManager<TurboUser> signinmanager;
        readonly UserManager<TurboUser> usermanager;
         public DashboardController(TurboDbContext db, SignInManager<TurboUser> signinmanager, UserManager<TurboUser> usermanager)
        {
            this.db = db;
            this.signinmanager = signinmanager;
            this.usermanager = usermanager;
         }
        [Authorize(Policy ="admin.dashboard.index")]
        public async Task<IActionResult> Index()
        {
            var data =await db.Announcements
                .Include(a => a.User)
                .Where(a => a.DeletedByUserId == null).ToListAsync();
            return View(data);
        }
        [HttpGet]
        [Authorize(Policy = "admin.dashboard.delete")]
        public IActionResult Delete(int id)
        {
            var data = db.Announcements
                .Include(a=>a.User)
                .First(a => a.Id == id);
            return View(data);
        }
        [HttpPost]
        [Authorize(Policy = "admin.dashboard.delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var elan = await db.Announcements.FindAsync(id);
            elan.DeletedByUserId = 1;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [Route("/signin.html")]
        public IActionResult SignIn()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/signin.html")]
        public async Task<IActionResult> SignIn(LoginUserModel user)
        {
            if (ModelState.IsValid)
            {
                TurboUser foundeduser = null;
                if (user.UserName.IsMail())
                {
                    foundeduser = await usermanager.FindByEmailAsync(user.UserName);
                }
                else
                {
                    foundeduser = await usermanager.FindByNameAsync(user.UserName);
                }
                if (foundeduser == null)
                {

                    ViewBag.Massage = ("Istifadeci adi ve ya sifreniz yalnisdir");
                    goto end;
                }
                var signnresult = await signinmanager.PasswordSignInAsync(foundeduser, user.Password, true, true);
                if (!signnresult.Succeeded)
                {
                    ViewBag.Massage = ("Istifadeci adi ve ya sifreniz yalnisdir");
                    goto end;
                }

                var callbackurl = Request.Query["ReturnUrl"];
                if (!string.IsNullOrWhiteSpace(callbackurl))
                {
                    return RedirectToPage(callbackurl);
                }
                
                    return RedirectToAction("index", "dashboard");
                
            }
        end:
            return View(user);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new TurboUser();
                user.Email = model.Email;
                user.UserName = model.Email;
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.EmailConfirmed = true;
                var result = await usermanager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ViewBag.Massage = "Qeydiyyat tamamlandi";
                    return RedirectToAction(nameof(SignIn));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

            }
            return View(model);
        }
        [Route("/logout.html")]
        public async Task<IActionResult> Logout()
        {
            await signinmanager.SignOutAsync();
            return RedirectToAction("signin", "dashboard");
        }
        [AllowAnonymous]
        [Route("/accessdenied.html")]
        public IActionResult Accessdeny()
        {
            return View();
        }

    }
}

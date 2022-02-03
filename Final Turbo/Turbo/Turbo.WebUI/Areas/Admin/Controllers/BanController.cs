using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turbo.WebUI.Models.DbContexts;
using Turbo.WebUI.Models.Entities;

namespace Turbo.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BanController : Controller
    {
        readonly TurboDbContext db;
        public BanController(TurboDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy ="admin.ban.index")]
        public async Task<IActionResult> Index()
        {
            var bans = await db.Bans.Where(b => b.DeletedByUserId == null).ToListAsync();
            return View(bans);
        }
        [Authorize(Policy ="admin.ban.details")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await db.Bans.FirstOrDefaultAsync(c => c.Id == id);
            return View(result);
        }
        [HttpGet]
        [Authorize(Policy = "admin.ban.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var ban = await db.Bans.FindAsync(id);
            return View(ban);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.ban.edit")]
        public async Task<IActionResult> Edit(int id, Ban ban)
        {
            if (id != ban.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                db.Update(ban);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(ban);
        }
        [Authorize(Policy = "admin.ban.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var ban = await db.Bans.FindAsync(id);
            return View(ban);
        }
        [Authorize(Policy = "admin.ban.delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var ban = await db.Bans.FindAsync(id);
            ban.DeletedByUserId = 1;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Policy = "admin.ban.create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.ban.create")]
        public async Task<IActionResult> Create(Ban ban)
        {
            if (ModelState.IsValid)
            {
                db.Add(ban);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ban);
        }
    }
}

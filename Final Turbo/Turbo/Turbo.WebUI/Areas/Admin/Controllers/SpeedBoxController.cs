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
    public class SpeedBoxController : Controller
    {

        readonly TurboDbContext db;
        public SpeedBoxController(TurboDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy = "admin.SpeedBox.index")]
        public async Task<IActionResult> Index()
        {
            var speedbox = await db.SpeedBoxs.Where(c => c.DeletedByUserId == null).ToListAsync();
            return View(speedbox);
        }
        [Authorize(Policy = "admin.SpeedBox.details")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await db.SpeedBoxs.FirstOrDefaultAsync(c => c.Id == id);
            return View(result);
        }
        [HttpGet]
        [Authorize(Policy = "admin.SpeedBox.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var speedbox = await db.SpeedBoxs.FindAsync(id);
            return View(speedbox);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.SpeedBox.edit")]
        public async Task<IActionResult> Edit(int id, SpeedBox speedbox)
        {
            if (id != speedbox.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                db.Update(speedbox);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(speedbox);
        }
        [Authorize(Policy = "admin.SpeedBox.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var speedbox = await db.SpeedBoxs.FindAsync(id);
            return View(speedbox);
        }
        [Authorize(Policy = "admin.SpeedBox.delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var speedbox = await db.SpeedBoxs.FindAsync(id);
            speedbox.DeletedByUserId = 1;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Policy = "admin.SpeedBox.create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.SpeedBox.create")]
        public async Task<IActionResult> Create(SpeedBox speedbox)
        {
            if (ModelState.IsValid)
            {
                db.Add(speedbox);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(speedbox);
        }
    }
}

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
    public class ColorController : Controller
    {

        readonly TurboDbContext db;
        public ColorController(TurboDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            var colors = await db.Colors.Where(c => c.DeletedByUserId == null).ToListAsync();
            return View(colors);
        }
        [Authorize(Policy = "admin.color.details")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await db.Colors.FirstOrDefaultAsync(c => c.Id == id);
            return View(result);
        }
        [HttpGet]
        [Authorize(Policy = "admin.color.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var color = await db.Colors.FindAsync(id);
            return View(color);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.color.edit")]
        public async Task<IActionResult> Edit(int id, Color color)
        {
            if (id != color.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                db.Update(color);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }
        [Authorize(Policy = "admin.color.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var color = await db.Colors.FindAsync(id);
            return View(color);
        }
        [Authorize(Policy = "admin.color.delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var color = await db.Colors.FindAsync(id);
            color.DeletedByUserId = 1;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Policy = "admin.color.create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.color.create")]
        public async Task<IActionResult> Create(Color color)
        {
            if (ModelState.IsValid)
            {
                db.Add(color);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }
    }
}

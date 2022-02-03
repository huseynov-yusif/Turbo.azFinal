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
    public class DistanceController : Controller
    {
        readonly TurboDbContext db;
        public DistanceController(TurboDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy = "admin.distance.index")]
        public async Task<IActionResult> Index()
        {
            var distance = await db.Distances.Where(b => b.DeletedByUserId == null).ToListAsync();
            return View(distance);
        }
        [Authorize(Policy = "admin.distance.details")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await db.Distances.FirstOrDefaultAsync(c => c.Id == id);
            return View(result);
        }
        [HttpGet]
        [Authorize(Policy = "admin.distance.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var distance = await db.Distances.FindAsync(id);
            return View(distance);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.distance.edit")]
        public async Task<IActionResult> Edit(int id, Distance distance)
        {
            if (id != distance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                db.Update(distance);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(distance);
        }
        [Authorize(Policy = "admin.distance.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var distance = await db.Distances.FindAsync(id);
            return View(distance);
        }
        [Authorize(Policy = "admin.distance.delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var distance = await db.Distances.FindAsync(id);
            distance.DeletedByUserId = 1;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Policy = "admin.distance.create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.distance.create")]
        public async Task<IActionResult> Create(Distance distance)
        {
            if (ModelState.IsValid)
            {
                db.Add(distance);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(distance);
        }
    }
}

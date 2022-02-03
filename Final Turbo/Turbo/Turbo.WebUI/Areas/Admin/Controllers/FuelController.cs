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
    public class FuelController : Controller
    {
        readonly TurboDbContext db;
        public FuelController(TurboDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy = "admin.fuel.index")]
        public async Task<IActionResult> Index()
        {
            var fuel = await db.Fuels.Where(b => b.DeletedByUserId == null).ToListAsync();
            return View(fuel);
        }
        [Authorize(Policy = "admin.fuel.details")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await db.Fuels.FirstOrDefaultAsync(c => c.Id == id);
            return View(result);
        }
        [HttpGet]
        [Authorize(Policy = "admin.fuel.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var fuel = await db.Fuels.FindAsync(id);
            return View(fuel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.fuel.edit")]
        public async Task<IActionResult> Edit(int id, Fuel fuel)
        {
            if (id != fuel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                db.Update(fuel);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(fuel);
        }
        [Authorize(Policy = "admin.fuel.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var fuel = await db.Fuels.FindAsync(id);
            return View(fuel);
        }
        [Authorize(Policy = "admin.fuel.delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var fuel = await db.Fuels.FindAsync(id);
            fuel.DeletedByUserId = 1;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Policy = "admin.fuel.create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.fuel.create")]
        public async Task<IActionResult> Create(Fuel fuel)
        {
            if (ModelState.IsValid)
            {
                db.Add(fuel);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fuel);
        }
    }
}

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
    public class SpesificationController : Controller
    {
        readonly TurboDbContext db;
        public SpesificationController(TurboDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy = "admin.spesification.index")]
        public async Task<IActionResult> Index()
        {
            var spesification = await db.Spesifications.Where(c => c.DeletedByUserId == null).ToListAsync();
            return View(spesification);
        }
        [Authorize(Policy = "admin.spesification.details")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await db.Spesifications.FirstOrDefaultAsync(c => c.Id == id);
            return View(result);
        }
        [HttpGet]
        [Authorize(Policy = "admin.spesification.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var spesification = await db.Spesifications.FindAsync(id);
            return View(spesification);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.spesification.edit")]
        public async Task<IActionResult> Edit(int id, Spesification spesification)
        {
            if (id != spesification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                db.Update(spesification);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(spesification);
        }
        [Authorize(Policy = "admin.spesification.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var spesifiction = await db.Spesifications.FindAsync(id);
            return View(spesifiction);
        }
        [Authorize(Policy = "admin.spesification.delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var spesification = await db.Spesifications.FindAsync(id);
            spesification.DeletedByUserId = 1;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Policy = "admin.spesification.create")]
        public IActionResult Create()
        {
            return View();
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.spesification.create")]
        public async Task<IActionResult> Create(Spesification spesification)
        {
            if (ModelState.IsValid)
            {
                db.Add(spesification);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spesification);
        }
    }
}

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
    public class BrandController : Controller
    {
        readonly TurboDbContext db;
        public BrandController(TurboDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy ="admin.brand.index")]
        public async Task<IActionResult> Index()
        {
            var brand = await db.Brands.Where(c => c.DeletedByUserId == null).ToListAsync();
            return View(brand);
        }
        [Authorize(Policy = "admin.brand.details")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await db.Brands.FirstOrDefaultAsync(c => c.Id == id);
            return View(result);
        }
        [HttpGet]
        [Authorize(Policy = "admin.brand.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await db.Brands.FindAsync(id);
            return View(brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.brand.edit")]
        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                db.Update(brand);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }
        [Authorize(Policy = "admin.brand.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await db.Brands.FindAsync(id);
            return View(brand);
        }
        [Authorize(Roles = "Superadmin")]
        [Authorize(Policy = "admin.brand.delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var brand = await db.Brands.FindAsync(id);
            brand.DeletedByUserId = 1;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Policy = "admin.brand.create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.brand.create")]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Add(brand);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }
    }
}

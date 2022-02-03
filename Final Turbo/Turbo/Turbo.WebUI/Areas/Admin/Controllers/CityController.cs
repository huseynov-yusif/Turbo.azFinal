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
    public class CityController : Controller
    {
        readonly TurboDbContext db;
        public CityController(TurboDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy = "admin.city.index")]
        public async Task<IActionResult> Index()
        {
            var cities = await db.Cities.Where(c => c.DeletedByUserId == null).ToListAsync(); 
            return View(cities);
        }
        [Authorize(Policy = "admin.brand.details")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await db.Cities.FirstOrDefaultAsync(c=>c.Id==id);
            return View(result);
        }
        [HttpGet]
        [Authorize(Policy = "admin.brand.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var city = await db.Cities.FindAsync(id);
            return View(city);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.brand.edit")]
        public async Task<IActionResult> Edit(int id, City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    db.Update(city);
                    await db.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }
        [Authorize(Policy = "admin.brand.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var city = await db.Cities.FindAsync(id);
            return View(city);
        }
        [Authorize(Policy = "admin.brand.delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var city = await db.Cities.FindAsync(id);
            city.DeletedByUserId = 1;
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
        public async Task<IActionResult> Create(City city)
        {
            if (ModelState.IsValid)
            {
                db.Add(city);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }
    }
}

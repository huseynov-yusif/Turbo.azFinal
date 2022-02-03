using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turbo.WebUI.Areas.Admin.Models.ViewModels;
using Turbo.WebUI.Models.DbContexts;
using Turbo.WebUI.Models.Entities;

namespace Turbo.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ModelController : Controller
    {
        readonly TurboDbContext db;
        public ModelController(TurboDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy = "admin.model.index")]
        public async Task<IActionResult> Index()
        {
            var models = await db.Models.Where(m => m.DeletedByUserId == null).ToListAsync();
            return View(models);
        }
        [Authorize(Policy = "admin.model.details")]
        public async Task<IActionResult> Details(int id)
        {
            var model = await db.Models.Include(m=>m.Brand)
                .FirstOrDefaultAsync(m=>m.Id==id);
            return View(model);
        }
        [Authorize(Policy = "admin.model.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await db.Models.FindAsync(id);
            return View(model);
        }
        [Authorize(Policy = "admin.model.delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var currency = await db.Models.FindAsync(id);
            currency.DeletedByUserId = 1;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    ModelViewModel viewmodel = new ModelViewModel ();
        //    viewmodel.Model =await db.Models.Include(m => m.Brand).FirstOrDefaultAsync(m => m.Id == id);
        //    viewmodel.Brands = await db.Brands.Where(b => b.DeletedByUserId == null).ToListAsync();
        //    return View(viewmodel);
        //}
        [HttpGet]
        [Authorize(Policy = "admin.model.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await db.Models.FindAsync(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.model.edit")]
        public async Task<IActionResult> Edit(int id,Model model)
        {

            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                db.Update(model);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "admin.model.create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.model.create")]
        public async Task<IActionResult> Create(Model model)
        {
            if (ModelState.IsValid)
            {
                db.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}

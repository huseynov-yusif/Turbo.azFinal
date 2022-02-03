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
    public class CurrencyController : Controller
    {

        readonly TurboDbContext db;
        public CurrencyController(TurboDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy = "admin.currency.index")]
        public async Task<IActionResult> Index()
        {
            var currency = await db.Currencys.Where(c => c.DeletedByUserId == null).ToListAsync();
            return View(currency);
        }
        [Authorize(Policy = "admin.currency.details")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await db.Currencys.FirstOrDefaultAsync(c => c.Id == id);
            return View(result);
        }
        [HttpGet]
        [Authorize(Policy = "admin.currency.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var currency = await db.Currencys.FindAsync(id);
            return View(currency);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.currency.edit")]
        public async Task<IActionResult> Edit(int id, Currency currency)
        {
            if (id != currency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                db.Update(currency);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }
        [Authorize(Policy = "admin.currency.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var currency = await db.Currencys.FindAsync(id);
            return View(currency);
        }
        [Authorize(Policy = "admin.currency.delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var currency = await db.Currencys.FindAsync(id);
            currency.DeletedByUserId = 1;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Policy = "admin.currency.create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.currency.create")]
        public async Task<IActionResult> Create(Currency currency)
        {
            if (ModelState.IsValid)
            {
                db.Add(currency);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }
    }
}

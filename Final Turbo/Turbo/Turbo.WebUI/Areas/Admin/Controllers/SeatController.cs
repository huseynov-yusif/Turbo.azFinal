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
    public class SeatController : Controller
    {

        readonly TurboDbContext db;
        public SeatController(TurboDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy = "admin.seat.index")]
        public async Task<IActionResult> Index()
        {
            var seat = await db.Seats.Where(c => c.DeletedByUserId == null).ToListAsync();
            return View(seat);
        }
        [Authorize(Policy = "admin.model.details")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await db.Seats.FirstOrDefaultAsync(c => c.Id == id);
            return View(result);
        }
        [HttpGet]
        [Authorize(Policy = "admin.model.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var seat = await db.Seats.FindAsync(id);
            return View(seat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.model.edit")]
        public async Task<IActionResult> Edit(int id, Seat seat)
        {
            if (id != seat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                db.Update(seat);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(seat);
        }
        [Authorize(Policy = "admin.model.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var seat = await db.Seats.FindAsync(id);
            return View(seat);
        }
        [Authorize(Policy = "admin.model.delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var seat = await db.Seats.FindAsync(id);
            seat.DeletedByUserId = 1;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Create(Seat seat)
        {
            if (ModelState.IsValid)
            {
                db.Add(seat);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seat);
        }
    }
}

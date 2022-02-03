using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turbo.WebUI.AppCode.Application.HomeModul;
using Turbo.WebUI.Models.DbContexts;
using Turbo.WebUI.Models.Entities;
using Turbo.WebUI.Models.ViewModels;

namespace Turbo.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        readonly TurboDbContext db;
        readonly IMediator mediator;
        public HomeController(TurboDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(int PageIndex=1,int PageSize=2)
        {
            var vm = new HomeViewModel();
            var announcements = db.Announcements
                .Include(a=>a.City)
                .Include(a=>a.Currency)
                .Include(a=>a.Distance)
                .Include(a=>a.Model)
                .Include(a=>a.Brand)
                .Include(a=>a.Photos.Where(a => a.IsMain == true))
                .Where(a=>a.DeletedByUserId==null);
            var pagedviewmodel = new PagedViewModel<Announcement>(announcements, PageIndex, PageSize);
            vm.Announcements = pagedviewmodel;
            vm.Cities = db.Cities
                .Where(c=>c.DeletedByUserId==null)
                .ToList();
            vm.Currencys = db.Currencys
                .Where(c=>c.DeletedByUserId==null)
                .ToList();
            vm.Brands = db.Brands
                .Where(c=>c.DeletedByUserId==null)
                .ToList();
            vm.Models = db.Models
                .Where(c => c.DeletedByUserId == null)
                .ToList();
            
                return View(vm);
        }
        public async Task<IActionResult> Details(int id)
        {
            var data = await db.Announcements
                 .Include(a => a.Ban)
                 .Include(a => a.City)
                 .Include(a => a.Color)
                 .Include(a => a.Currency)
                 .Include(a => a.Distance)
                 .Include(a => a.Model)
                 .Include(a => a.Fuel)
                 .Include(a => a.Seat)
                 .Include(a => a.SpeedBox)
                 .Include(a => a.Brand)
                 .Include(a => a.Photos)
                 .Include(a => a.User)
                 .Include(a => a.AnnouncementSpesificationItems.Where(s=>s.AnnouncementId==id)).ThenInclude(a=>a.Spesification)
                 .FirstOrDefaultAsync(a => a.Id == id && a.DeletedByUserId == null);

            return View(data);
        }

        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            var announcements = db.Announcements
                .Include(a => a.City)
                .Include(a => a.Currency)
                .Include(a => a.Distance)
                .Include(a => a.Model)
                .Include(a => a.Brand)
                .Include(a => a.Photos.Where(a => a.IsMain == true)).Where(a =>
            (model.BrandId != 0 ? a.BrandId == model.BrandId : true) &&
            (model.ModelId != 0 ? a.ModelId == model.ModelId : true) &&
            (model.CityId != 0 ? a.CityId == model.CityId : true) &&
            (model.CurrencyId != 0 ? a.CurrencyId == model.CurrencyId : true) &&
            (model.MinPrice != null ? a.Price >= model.MinPrice : true) &&
            (model.MaxPrice != null ? a.Price <= model.MaxPrice : true) &&
            (model.MinYear != null ? a.Year >= model.MinYear : true) &&
            (model.MaxYear != null ? a.Year <= model.MaxYear : true) &&
            (model.Change == true ? a.Change == model.Change : true) &&
            (model.Credit == true ? a.Credit == model.Credit : true))
                .ToList();
            return View(announcements);
        }
        public async Task<IActionResult> SearchModel(int id)
        {
            var models = await db.Models.Where(m => m.BrandId == id).ToListAsync();
            return Json(models);
        }

        public IActionResult Low()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Useragreement()
        {
            return View();
        }
        public IActionResult Rules()
        {
            return View();
        }
        
        [HttpGet]

        public IActionResult Create()
        {
            AnnouncementCreateCommand cvm = new AnnouncementCreateCommand();
            cvm.Citys = db.Cities
               .Where(c => c.DeletedByUserId == null)
               .ToList();
            cvm.Currencys = db.Currencys
                .Where(c => c.DeletedByUserId == null)
                .ToList();
            cvm.Brands = db.Brands
                .Where(c => c.DeletedByUserId == null)
                .ToList();
            cvm.Models = db.Models
                .Where(c => c.DeletedByUserId == null)
                .ToList();
            cvm.Fuels = db.Fuels
               .Where(c => c.DeletedByUserId == null)
               .ToList();
            cvm.SpeedBox = db.SpeedBoxs
               .Where(c => c.DeletedByUserId == null)
               .ToList();
            cvm.Bans = db.Bans
               .Where(c => c.DeletedByUserId == null)
               .ToList();
            cvm.Colors = db.Colors
                .Where(c => c.DeletedByUserId == null)
                .ToList();
            cvm.Spesifications = db.Spesifications
               .Where(c => c.DeletedByUserId == null)
               .ToList();
            cvm.Seats = db.Seats
               .Where(c => c.DeletedByUserId == null)
               .ToList();
            cvm.Distances = db.Distances
               .Where(c => c.DeletedByUserId == null)
               .ToList();

            return View(cvm);
        }
        [HttpPost]
        //public IActionResult Create(CreateViewModel model)
        //{
        //    try
        //    {
        //        var user = new User();
        //        user.Name = model.User.Name;
        //        user.Phone = model.User.Phone;
        //        user.Email = model.User.Email;
        //        db.User.Add(user);
        //        db.SaveChanges();
        //        var newuser = db.User.FirstOrDefault(u => u.Email == model.User.Email);

        //        var elan = new Announcement();
        //        elan.UserId = newuser.Id;
        //        elan.BanId = model.Announcement.BanId;
        //        elan.BrandId = model.Announcement.BrandId;
        //        elan.CityId = model.Announcement.CityId;
        //        elan.ColorId = model.Announcement.ColorId;
        //        elan.CurrencyId = model.Announcement.CurrencyId;
        //        elan.DistanceId = model.Announcement.DistanceId;
        //        elan.FuelId = model.Announcement.FuelId;
        //        elan.ModelId = model.Announcement.ModelId;
        //        elan.SeatId = model.Announcement.SeatId;
        //        elan.SpeedBoxId = model.Announcement.SpeedBoxId;
        //        elan.Year = model.Announcement.Year;
        //        elan.March = model.Announcement.March;
        //        elan.Price = model.Announcement.Price;
        //        elan.EngineStrong = model.Announcement.EngineStrong;
        //        elan.EngineVolume = model.Announcement.EngineVolume;
        //        elan.Pin = model.Announcement.Pin;
        //        elan.Description = model.Announcement.Description;

        //        db.Announcements.Add(elan);
        //        db.SaveChanges();
        //        var newannouncement = db.Announcements.FirstOrDefault(a =>a.Pin == model.Announcement.Pin);
        //        var photo = new Photo();
        //        photo.ImagePath = model.Photo.ImagePath;
        //        photo.IsMain = true;
        //        photo.AnnouncementId = newannouncement.Id;
        //        db.Photos.Add(photo);
        //        db.SaveChanges();
        //        return RedirectToAction("index", "home");
        //    }
        //    catch (Exception)
        //    {

        //        return RedirectToAction("create", "home");
        //    }

        //}
        public async Task<IActionResult> Create(AnnouncementCreateCommand request)
        {
            try
            {
               var a= await mediator.Send(request);
                if (a > 0)
                {
                    return RedirectToAction("index", "home");
                }

                else
                {
                    return RedirectToAction("create", "home");
                }
                }
            catch (Exception)
            {

                return RedirectToAction("create", "home");
            }

        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(Announcement model)
        {
            try
            {
                var datas = db.Announcements.FirstOrDefault(x => x.Pin == model.Pin);
                db.Announcements.Remove(datas);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                return RedirectToAction("delete", "Home");
            }
        }
    }
}

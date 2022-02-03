using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Turbo.WebUI.Models.DbContexts;
using Turbo.WebUI.Models.Entities;
using Turbo.WebUI.Models.FormModels;
using Turbo.WebUI.Models.ViewModels;

namespace Turbo.WebUI.AppCode.Application.HomeModul
{
    public class AnnouncementCreateCommand:IRequest<int>
    {
        public Announcement Announcement { get; set; }
        public List<Spesification> Spesifications { get; set; }
        public List<SpeedBox> SpeedBox { get; set; }
        public List<Seat> Seats { get; set; }
        public List<Model> Models { get; set; }
        public List<Fuel> Fuels { get; set; }
        public List<Distance> Distances { get; set; }
        public List<Currency> Currencys { get; set; }
        public List<Color> Colors { get; set; }
        public List<City> Citys { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Ban> Bans { get; set; }
        public User User { get; set; }
        public ImageItemFormModel[] images { get; set; }
        public ICollection<Photo> Images { get; set; }
        public class AnnouncementCreateCommandHandler : IRequestHandler<AnnouncementCreateCommand,int>
        {

            readonly TurboDbContext db;
            readonly IWebHostEnvironment env;
            public AnnouncementCreateCommandHandler(TurboDbContext db, IWebHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<int> Handle(AnnouncementCreateCommand request, CancellationToken cancellationToken)
            {
                var user = new User();
                user.Name = request.User.Name;
                user.Phone = request.User.Phone;
                user.Email = request.User.Email;
                db.User.Add(user);
                await db.SaveChangesAsync();

                var elan = new Announcement();
                elan.UserId = user.Id;
                elan.BanId = request.Announcement.BanId;
                elan.BrandId = request.Announcement.BrandId;
                elan.CityId = request.Announcement.CityId;
                elan.ColorId = request.Announcement.ColorId;
                elan.CurrencyId = request.Announcement.CurrencyId;
                elan.DistanceId = request.Announcement.DistanceId;
                elan.FuelId = request.Announcement.FuelId;
                elan.ModelId = request.Announcement.ModelId;
                elan.SeatId = request.Announcement.SeatId;
                elan.SpeedBoxId = request.Announcement.SpeedBoxId;
                elan.Year = request.Announcement.Year;
                elan.March = request.Announcement.March;
                elan.Price = request.Announcement.Price;
                elan.EngineStrong = request.Announcement.EngineStrong;
                elan.EngineVolume = request.Announcement.EngineVolume;
                elan.Pin = request.Announcement.Pin;
                elan.Description = request.Announcement.Description;

                var k = 0;

                elan.Photos = new List<Photo>();
                foreach (var image in request.images.Where(i => i.File != null))
                {
                    string extension = Path.GetExtension(image.File.FileName); //.jpg
                    string imagePath = $"{DateTime.Now:yyyyMMddHHmmss}-{Guid.NewGuid()}{extension}";
                    string physicalPath = Path.Combine(env.ContentRootPath,
                        "wwwroot",
                        "assets",
                        "Uploads",
                        "Carphotos",
                        imagePath);

                    using (var stream = new FileStream(physicalPath, FileMode.Create, FileAccess.Write))
                    {
                        await image.File.CopyToAsync(stream);
                    }
                    if (k==0)
                    {
                        elan.Photos.Add(new Photo
                        {
                            IsMain = true,
                            ImagePath = imagePath
                        });
                        k = 1;
                    }
                    else
                    {
                        elan.Photos.Add(new Photo
                        {
                            IsMain = image.IsMain,
                            ImagePath = imagePath
                        });
                    }
                }
                db.Announcements.Add(elan);
                await db.SaveChangesAsync();

                

                return elan.Id;

            }
        }
    }
}

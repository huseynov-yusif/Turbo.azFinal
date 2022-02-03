using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turbo.WebUI.Models.Entities;

namespace Turbo.WebUI.Models.ViewModels
{
    public class CreateViewModel
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
        public Photo Photo { get; set; }
    }
}

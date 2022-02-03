using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turbo.WebUI.Models.Entities;

namespace Turbo.WebUI.Models.ViewModels
{
    public class HomeViewModel
    {
        public PagedViewModel<Announcement> Announcements { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Model> Models { get; set; }
        public List<City> Cities { get; set; }
        public List<Currency> Currencys { get; set; }

    }
}

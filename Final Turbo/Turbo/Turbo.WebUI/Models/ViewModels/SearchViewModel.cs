using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turbo.WebUI.Models.ViewModels
{
    public class SearchViewModel
    {
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int CurrencyId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? MaxYear { get; set; }
        public int? MinYear { get; set; }
        public int CityId { get; set; }
        public bool Credit { get; set; }
        public bool Change { get; set; }
    }
}

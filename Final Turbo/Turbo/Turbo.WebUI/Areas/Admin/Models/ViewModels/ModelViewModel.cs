using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turbo.WebUI.Models.Entities;

namespace Turbo.WebUI.Areas.Admin.Models.ViewModels
{
    public class ModelViewModel
    {
        public List<Brand> Brands { get; set; }
        public Model Model { get; set; }
    }
}

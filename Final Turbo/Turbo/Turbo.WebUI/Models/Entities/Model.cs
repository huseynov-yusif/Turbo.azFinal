using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turbo.WebUI.Models.Entities
{
    public class Model:BaseEntity
    {
        public string Name { get; set; }
        public int? BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}

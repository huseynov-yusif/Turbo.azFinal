using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turbo.WebUI.Models.Entities
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Model> Models{ get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turbo.WebUI.Models.Entities
{
    public class Spesification:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<AnnouncementSpesificationItem> AnnouncementSpesificationItems { get; set; }
    }
}

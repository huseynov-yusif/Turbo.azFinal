using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turbo.WebUI.Models.Entities
{
    public class AnnouncementSpesificationItem:BaseEntity
    {
        public int AnnouncementId { get; set; }
        public virtual Announcement Announcement { get; set; }
        public int SpesificationId { get; set; }
        public virtual Spesification Spesification { get; set; }
    }
}

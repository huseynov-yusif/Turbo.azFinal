using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turbo.WebUI.Models.Entities
{
    public class Photo:BaseEntity
    {
        public int AnnouncementId { get; set; }
        public virtual Announcement Announcement { get; set; }
        public string ImagePath { get; set; }
        public bool IsMain { get; set; }
    }
}

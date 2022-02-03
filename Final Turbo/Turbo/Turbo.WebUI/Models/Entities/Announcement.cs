using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Turbo.WebUI.Models.Entities
{
    public class Announcement:BaseEntity
    {
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public int? ModelId { get; set; }
        public virtual Model Model { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int BanId { get; set; }
        public virtual Ban Ban { get; set; }
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public int DistanceId { get; set; }
        public virtual Distance Distance { get; set; }
        public int FuelId { get; set; }
        public virtual Fuel Fuel { get; set; }
        public int SeatId { get; set; }
        public virtual Seat Seat  { get; set; }
        public int SpeedBoxId { get; set; }
        public virtual SpeedBox SpeedBox { get; set; }
        public int Price { get; set; }
        public string March { get; set; }
        public int Year { get; set; }
        public int EngineVolume { get; set; }
        public int EngineStrong { get; set; }
        public bool Credit { get; set; }
        public bool Change { get; set; }
        public string Description { get; set; }
        public int Pin { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<AnnouncementSpesificationItem> AnnouncementSpesificationItems { get; set; }

    }
}

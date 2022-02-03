using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Turbo.WebUI.Models.Entities;
using Turbo.WebUI.Models.Entities.Membership;

namespace Turbo.WebUI.Models.DbContexts
{
    public class TurboDbContext:IdentityDbContext<TurboUser,TurboRole,int,TurboUserClaim,TurboUserRole,TurboUserLogin,TurboRoleClaim,TurboUserToken>
    {
        public TurboDbContext()
            : base()
        {

        }
        public TurboDbContext(DbContextOptions options)
            :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TurboUser>(e => {
                e.ToTable("TurboUsers", "Membership");
            });
            builder.Entity<TurboRole>(e => {
                e.ToTable("TurboRoles", "Membership");
            });
            builder.Entity<TurboUserClaim>(e => {
                e.ToTable("TurboUserClaims", "Membership");
            });
            builder.Entity<TurboRoleClaim>(e => {
                e.ToTable("TurboRoleClaims", "Membership");
            });
            builder.Entity<TurboUserRole>(e => {
                e.ToTable("TurboUserRoles", "Membership");
            });
            builder.Entity<TurboUserLogin>(e => {
                e.ToTable("TurboUserLogins", "Membership");
            });
            builder.Entity<TurboUserToken>(e => {
                e.ToTable("TurboUserTokens", "Membership");
            });
        }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementSpesificationItem> AnnouncementSpesificationItems { get; set; }
        public DbSet<Ban> Bans { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Currency> Currencys { get; set; }
        public DbSet<Distance> Distances { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SpeedBox> SpeedBoxs { get; set; }
        public DbSet<Spesification> Spesifications { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<User> User { get; set; }

    }
}

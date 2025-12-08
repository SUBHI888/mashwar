using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace mashwar.Models
{
    public class AppDbContext:IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) 
        {
            
        }
        public DbSet<Place> places { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BusinessLicense> BusinessLicenses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<WorkTime> WorkTimes { get; set; }
        public DbSet<Category> categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ العلاقة بين Place و AppUser
            modelBuilder.Entity<Place>()
                .HasOne(p => p.Users)
                .WithMany(u => u.Places)
                .HasForeignKey(p => p.User_Id)
                .OnDelete(DeleteBehavior.NoAction);

            // ✅ العلاقة بين Booking و Place
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Place)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PlaceId)
                .OnDelete(DeleteBehavior.NoAction);

            // ✅ العلاقة بين Booking و AppUser
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // ✅ العلاقة بين Review و Place
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Place)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.PlaceId)
                .OnDelete(DeleteBehavior.NoAction);

            // ✅ العلاقة بين Review و AppUser
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // ✅ العلاقة بين BusinessLicense و AppUser
            modelBuilder.Entity<BusinessLicense>()
                .HasOne(b => b.User)
                .WithMany(u => u.BusinessLicenses)
                .HasForeignKey(b => b.User_Id)
                .OnDelete(DeleteBehavior.NoAction);

            // ✅ العلاقة بين Notification و AppUser
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // ✅ WorkTime ↔ Place
            modelBuilder.Entity<WorkTime>()
                .HasOne(w => w.Place)
                .WithMany(p => p.WorkTimes)
                .HasForeignKey(w => w.PlaceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Category>()
              .HasOne(b => b.Place)
              .WithMany(p => p.categories)
              .HasForeignKey(b => b.PlaceId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Category>()
              .HasOne(r => r.User)
              .WithMany(u => u.categories)
              .HasForeignKey(r => r.UserId)
              .OnDelete(DeleteBehavior.NoAction);


        }
    }
}

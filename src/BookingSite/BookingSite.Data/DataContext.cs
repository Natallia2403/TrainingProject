using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BookingSite.Data.Configuration;
using BookingSite.Data.Models;

namespace BookingSite.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new BookingConfiguration());

            modelBuilder.ApplyConfiguration(new EventLogConfiguration());

            modelBuilder.ApplyConfiguration(new CountryConfiguration());

            modelBuilder.ApplyConfiguration(new HotelConfiguration());

            modelBuilder.ApplyConfiguration(new RoomConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

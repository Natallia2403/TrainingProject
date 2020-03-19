﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingProject.Data.Configuration;
using TrainingProject.Data.Models;

namespace TrainingProject.Data
{
    public class DataContext: DbContext
    {
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new BookingConfiguration());

            modelBuilder.ApplyConfiguration(new CityConfiguration());

            modelBuilder.ApplyConfiguration(new ClientConfiguration());

            modelBuilder.ApplyConfiguration(new CountryConfiguration());

            modelBuilder.ApplyConfiguration(new GalleryConfiguration());

            modelBuilder.ApplyConfiguration(new HotelConfiguration());

            modelBuilder.ApplyConfiguration(new PaymentConfiguration());

            modelBuilder.ApplyConfiguration(new RoomConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using BookingSite.Data.Models;

namespace BookingSite.Data.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Tables.Booking);

            builder.HasKey(ag => ag.Id);

            builder.Property(ag => ag.Id)
                    .ValueGeneratedOnAdd();

            builder.HasOne(ag => ag.Room)//Методы HasOne и HasMany устанавливают навигационное свойство для сущности, для которой производится конфигурация
                    .WithMany(t => t.Bookings)//методы WithOne и WithMany идентифицируют навигационное свойство на стороне связанной сущности
                    .HasForeignKey(p => p.RoomId)//внешний ключ
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ag => ag.User)//Методы HasOne и HasMany устанавливают навигационное свойство для сущности, для которой производится конфигурация
                    .WithMany(t => t.Bookings)//методы WithOne и WithMany идентифицируют навигационное свойство на стороне связанной сущности
                    .HasForeignKey(p => p.UserId)//внешний ключ
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

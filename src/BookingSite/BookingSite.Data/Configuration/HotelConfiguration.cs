﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using BookingSite.Data.Models;

namespace BookingSite.Data.Configuration
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Tables.Hotel);

            builder.HasKey(ag => ag.Id);

            builder.Property(ag => ag.Id)
                    .ValueGeneratedOnAdd();

            builder.HasOne(ag => ag.Country)//Методы HasOne и HasMany устанавливают навигационное свойство для сущности, для которой производится конфигурация
                    .WithMany(t => t.Hotels)//методы WithOne и WithMany идентифицируют навигационное свойство на стороне связанной сущности
                    .HasForeignKey(p => p.CountryId)//внешний ключ
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ag => ag.User)//Методы HasOne и HasMany устанавливают навигационное свойство для сущности, для которой производится конфигурация
                    .WithMany(t => t.Hotels)//методы WithOne и WithMany идентифицируют навигационное свойство на стороне связанной сущности
                    .HasForeignKey(p => p.UserId)//внешний ключ
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingProject.Data.Models;

namespace TrainingProject.Data.Configuration
{
    public class GalleryConfiguration : IEntityTypeConfiguration<Gallery>
    {
        public void Configure(EntityTypeBuilder<Gallery> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Tables.Gallery);

            builder.HasKey(ag => ag.Id);

            builder.Property(ag => ag.Id)
                    .ValueGeneratedOnAdd();

            builder.HasOne(ag => ag.Hotel)//Методы HasOne и HasMany устанавливают навигационное свойство для сущности, для которой производится конфигурация
                    .WithMany(t => t.Galleries)//методы WithOne и WithMany идентифицируют навигационное свойство на стороне связанной сущности
                    .HasForeignKey(p => p.HotelId)//внешний ключ
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

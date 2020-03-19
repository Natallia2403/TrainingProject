using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingProject.Data.Models;

namespace TrainingProject.Data.Configuration
{
    class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Tables.Client);

            builder.HasKey(ag => ag.Id);

            builder.Property(ag => ag.Id)
                    .ValueGeneratedOnAdd();

            builder.HasOne(ag => ag.Country)//Методы HasOne и HasMany устанавливают навигационное свойство для сущности, для которой производится конфигурация
                    .WithMany(t => t.Clients)//методы WithOne и WithMany идентифицируют навигационное свойство на стороне связанной сущности
                    .HasForeignKey(p => p.CountryId)//внешний ключ
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

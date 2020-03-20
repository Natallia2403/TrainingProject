using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingProject.Data.Models;

namespace TrainingProject.Data.Configuration
{
    public class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Tables.EventLog);

            builder.HasKey(ag => ag.Id);

            builder.Property(ag => ag.Id)
                    .ValueGeneratedOnAdd();
        }
    }
}

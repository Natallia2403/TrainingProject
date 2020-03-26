using BookingSite.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookingSite.Data
{
    public interface IDataContext
    {
        DbSet<Booking> Bookings { get; set; }

        DbSet<Client> Clients { get; set; }

        DbSet<Country> Countries { get; set; }

        DbSet<Gallery> Galleries { get; set; }

        DbSet<Hotel> Hotels { get; set; }

        DbSet<Payment> Payments { get; set; }

        DbSet<Room> Rooms { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

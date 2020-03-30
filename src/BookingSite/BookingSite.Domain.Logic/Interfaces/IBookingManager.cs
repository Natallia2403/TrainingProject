using BookingSite.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSite.Domain.Logic.Interfaces
{
    public interface IBookingManager
    {
        Task AddAsync(BookingDTO dto);
    }
}

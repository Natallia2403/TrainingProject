using BookingSite.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSite.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task AddAsync(BookingDTO dto);

        Task<bool> IsCanBeBookedAsync(int? id, DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<BookingDTO>> GetByRoomIdAsync(int roomId);

        Task<IEnumerable<BookingDTO>> GetByUserId(string userId);

        Task<BookingDTO> GetByIdAsync(int? id);

        Task DeleteAsync(int? id);

        Task DeleteAsync(BookingDTO dto);
    }
}

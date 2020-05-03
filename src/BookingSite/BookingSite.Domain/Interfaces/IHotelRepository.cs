using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;

namespace BookingSite.Domain.Interfaces
{
    public interface IHotelRepository
    {
        Task<IEnumerable<HotelDTO>> GetAllAsync();

        Task<IEnumerable<HotelDTO>> GetByUserIdAsync(string userId);

        Task<HotelDTO> GetByIdAsync(int? id);

        Task AddAsync(HotelDTO dto);

        Task UpdateAsync(HotelDTO dto);

        Task DeleteAsync(int? id);

        Task DeleteAsync(HotelDTO dto);
    }
}

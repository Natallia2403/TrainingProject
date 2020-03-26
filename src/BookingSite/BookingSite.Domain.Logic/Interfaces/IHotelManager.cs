using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;

namespace BookingSite.Domain.Logic.Interfaces
{
    public interface IHotelManager
    {
        Task AddAsync(Hotel hotel);

        Task<IEnumerable<Hotel>> GetAllAsync();

        Task<Hotel> GetByIdAsync(int? id);

        Task UpdateAsync(Hotel hotel);

        Task DeleteAsync(int? id);

        Task DeleteAsync(Hotel id);
    }
}

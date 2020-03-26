using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;

namespace BookingSite.Domain.Logic.Interfaces
{
    public interface IRoomManager
    {
        Task AddAsync(Room room);

        Task<IEnumerable<Room>> GetAllAsync();

        Task<Room> GetByIdAsync(int? id);

        Task UpdateAsync(Room room);

        Task DeleteAsync(int? id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;

namespace BookingSite.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<RoomDTO>> GetAllAsync();

        Task<RoomDTO> GetByIdAsync(int? id);

        Task AddAsync(RoomDTO dto);

        Task UpdateAsync(RoomDTO dto);

        Task DeleteAsync(int? id);

        Task DeleteAsync(RoomDTO dto);
    }
}

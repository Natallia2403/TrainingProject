using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Domain.Logic.Interfaces;

namespace BookingSite.Domain.Logic.Managers
{
    public class RoomManager : IRoomManager
    {
        DataContext _dataContext;

        public RoomManager(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _dataContext.Rooms.AsNoTracking().Include(u => u.Hotel).ToListAsync();
        }

        public async Task<Room> GetByIdAsync(int? id)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            var room = await _dataContext.Rooms.AsNoTracking().Include(u => u.Hotel).FirstOrDefaultAsync(h => h.Id == id);

            if (room != null)
                return room;
            throw new Exception("Комната не найдена");
        }

        public async Task AddAsync(Room room)
        {
            room = room ?? throw new ArgumentNullException(nameof(room));

            _dataContext.Rooms.Add(room);

            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Room room)
        {
            room = room ?? throw new ArgumentNullException(nameof(room));

            _dataContext.Rooms.Update(room);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            var room = await _dataContext.Rooms.AsNoTracking().Include(u => u.Hotel).FirstOrDefaultAsync(h => h.Id == id);

            if (room != null)
            {
                _dataContext.Rooms.Remove(room);

                await _dataContext.SaveChangesAsync();
            }
            else
                throw new Exception("Комната не найдена");
        }
    }
}

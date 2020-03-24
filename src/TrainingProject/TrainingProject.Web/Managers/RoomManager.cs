using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingProject.Data;
using TrainingProject.Data.Models;
using TrainingProject.Web.Interfaces;

namespace TrainingProject.Web.Managers
{
    public class RoomManager : IRoomManager
    {
        DataContext _dataContext;

        public RoomManager(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public IEnumerable<Room> GetAll()
        {
            return _dataContext.Rooms.ToList();
        }

        public Room GetById(int? id)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            Room room = _dataContext.Rooms.Include(u => u.Hotel).FirstOrDefault(h => h.Id == id);
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

            Room room = GetById(id);
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

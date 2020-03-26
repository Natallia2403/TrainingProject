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
    public class HotelManager : IHotelManager
    {
        DataContext _dataContext;

        public HotelManager(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task AddAsync(Hotel hotel)
        {
            hotel = hotel ?? throw new ArgumentNullException(nameof(hotel));

            _dataContext.Hotels.Add(hotel);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _dataContext.Hotels.AsNoTracking().Include(u => u.Country).Include(u => u.Rooms).ToListAsync();
        }

        public async Task<Hotel> GetByIdAsync(int? id)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            Hotel hotel = await _dataContext.Hotels.AsNoTracking().Include(u => u.Country).Include(u => u.Rooms).FirstOrDefaultAsync(h => h.Id == id);
            if (hotel != null)
                return hotel;
            throw new Exception("Отель не найден");
        }

        public async Task UpdateAsync(Hotel hotel)
        {
            hotel = hotel ?? throw new ArgumentNullException(nameof(hotel));

            _dataContext.Hotels.Update(hotel);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            Hotel hotel = await _dataContext.Hotels.AsNoTracking().Include(u => u.Country).Include(u => u.Rooms).FirstOrDefaultAsync(h => h.Id == id);

            if (hotel != null)
            {
                if(hotel.Rooms != null && hotel.Rooms.Count > 0)
                {
                    throw new Exception("Пожалуйста, сначала удалите комнаты");
                }

                _dataContext.Hotels.Remove(hotel);

                await _dataContext.SaveChangesAsync();
            }
            else
                throw new Exception("Отель не найден");
        }

        public async Task DeleteAsync(Hotel hotel)
        {
            hotel = hotel ?? throw new ArgumentNullException(nameof(hotel));

            _dataContext.Hotels.Remove(hotel);

            await _dataContext.SaveChangesAsync();
        }
    }
}

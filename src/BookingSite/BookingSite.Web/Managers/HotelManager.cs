using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Web.Interfaces;
using BookingSite.Web.ViewModels;

namespace BookingSite.Web.Managers
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

        public IEnumerable<Hotel> GetAll()
        {
            return _dataContext.Hotels.Include(u => u.Country).Include(u => u.Rooms).ToList();
        }

        public Hotel GetById(int? id)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            Hotel hotel = _dataContext.Hotels.Include(u => u.Country).Include(u => u.Rooms).FirstOrDefault(h => h.Id == id);
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

            Hotel hotel = GetById(id);
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

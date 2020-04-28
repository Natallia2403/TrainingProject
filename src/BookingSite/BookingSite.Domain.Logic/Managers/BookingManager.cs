using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;
using BookingSite.Domain.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSite.Domain.Logic.Managers
{
    public class BookingManager : IBookingManager
    {
        DataContext _dataContext;
        IMapper _mapper;

        public BookingManager(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddAsync(BookingDTO dto)
        {
            dto = dto ?? throw new ArgumentNullException(nameof(dto));

            var model = _mapper.Map<Booking>(dto);

            _dataContext.Bookings.Add(model);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> IsCanBeBookedAsync(int? roomId, DateTime dateFrom, DateTime dateTo)
        {
            roomId = roomId ?? throw new ArgumentNullException(nameof(roomId));

            var count = await _dataContext.Bookings.AsNoTracking()
                            .Where(b => b.RoomId == roomId && b.DateFrom >= dateFrom && b.DateTo <= dateTo)
                            .CountAsync();

            var isCanBeBooked = count == 0;

            return isCanBeBooked;
        }

        public async Task<IEnumerable<BookingDTO>> GetByRoomIdAsync(int roomId)
        {
            var models = _dataContext.Bookings.AsNoTracking()
                .Include(r => r.Room)
                .Include(u => u.User)
                .Where(r => r.RoomId == roomId);

            var dto = await _mapper.ProjectTo<BookingDTO>(models).ToListAsync();

            return dto;
        }

        public async Task<IEnumerable<BookingDTO>> GetByUserId(string userId)
        {
            var models = _dataContext.Bookings.AsNoTracking()
                .Include(r => r.Room)
                .Include(h => h.Room.Hotel)
                .Where(r => r.UserId == userId)
                .OrderByDescending(o => o.DateFrom);

            var dto = await _mapper.ProjectTo<BookingDTO>(models).ToListAsync();

            return dto;
        }

        public async Task<BookingDTO> GetByIdAsync(int? id)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            var model = await _dataContext.Bookings.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            if (model != null)
            {
                var dto = _mapper.Map<BookingDTO>(model);
                return dto;
            }
            throw new Exception("Бронь не найдена");
        }

        public async Task DeleteAsync(int? id)
        {
            id = id ?? throw new ArgumentNullException(nameof(id));

            var dto = await GetByIdAsync(id);

            await DeleteAsync(dto);
        }

        public async Task DeleteAsync(BookingDTO dto)
        {
            dto = dto ?? throw new ArgumentNullException(nameof(dto));

            var model = _mapper.Map<Booking>(dto);

            _dataContext.Bookings.Remove(model);

            await _dataContext.SaveChangesAsync();
        }
    }
}

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

        public bool IsCanBeBooked(int? roomId, DateTime dateFrom, DateTime dateTo)
        {
            roomId = roomId ?? throw new ArgumentNullException(nameof(roomId));

            var count = _dataContext.Bookings.AsNoTracking()
                            .Where(b => b.RoomId == roomId && b.DateFrom >= dateFrom && b.DateTo <= dateTo)
                            .Count();

            var isCanBeBooked = count == 0;

            return isCanBeBooked;
        }
    }
}

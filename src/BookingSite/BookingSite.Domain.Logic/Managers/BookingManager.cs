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
    }
}

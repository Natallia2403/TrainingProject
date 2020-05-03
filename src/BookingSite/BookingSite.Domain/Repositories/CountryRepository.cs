using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;
using BookingSite.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSite.Domain.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        DataContext _dataContext;
        IMapper _mapper;

        public CountryRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CountryDTO>> GetAllAsync()
        {
            var model = _dataContext.Countries.AsNoTracking();
            var dto = await _mapper.ProjectTo<CountryDTO>(model).ToListAsync();
            return dto;
        }
    }
}


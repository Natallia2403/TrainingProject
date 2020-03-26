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
    public class CountryManager : ICountryManager
    {
        DataContext _dataContext;
        IMapper _mapper;

        public CountryManager(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            var countries = await _dataContext.Countries.AsNoTracking().ToListAsync();
            //var dto = await _mapper.ProjectTo<CountryDTO>(countries).ToListAsync();
            //var dto = _mapper.Map<IQueryable<CountryDTO>>(countries);
            return countries;
        }
    }
}


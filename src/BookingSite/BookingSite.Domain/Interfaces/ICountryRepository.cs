using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;

namespace BookingSite.Domain.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<CountryDTO>> GetAllAsync();
    }
}

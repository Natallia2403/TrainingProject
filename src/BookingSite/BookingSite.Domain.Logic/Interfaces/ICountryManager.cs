using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;

namespace BookingSite.Domain.Logic.Interfaces
{
    public interface ICountryManager
    {
        Task<IEnumerable<CountryDTO>> GetAllAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;

namespace BookingSite.Web.Interfaces
{
    public interface ICountryManager
    {
        IEnumerable<Country> GetAll();
    }
}

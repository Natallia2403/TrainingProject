using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;
using BookingSite.Web.ViewModels;

namespace BookingSite.Web.Interfaces
{
    public interface IHotelManager
    {
        Task AddAsync(Hotel hotel);

        IEnumerable<Hotel> GetAll();

        Hotel GetById(int? id);

        Task UpdateAsync(Hotel hotel);

        Task DeleteAsync(int? id);

        Task DeleteAsync(Hotel id);
    }
}

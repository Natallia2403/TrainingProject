using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;

namespace BookingSite.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<HotelDTO> Hotels { get; set; }
    }
}

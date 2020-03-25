using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;

namespace BookingSite.Web.ViewModels
{
    public class HotelViewModel
    {
        public IEnumerable<Country> Countries { get; set; }

        public Hotel Hotel { get; set; }
    }
}

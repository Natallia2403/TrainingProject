using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;

namespace BookingSite.Web.ViewModels
{
    public class HotelViewModel
    {
        //public IEnumerable<Country> Countries { get; set; }//TODO

        public IEnumerable<CountryDTO> Countries { get; set; }

        public HotelDTO Hotel { get; set; }
    }
}

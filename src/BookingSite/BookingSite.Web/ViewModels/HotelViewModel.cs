using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Common.Enums;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;

namespace BookingSite.Web.ViewModels
{
    public class HotelViewModel
    {
        public IEnumerable<CountryDTO> Countries { get; set; }

        public HotelDTO Hotel { get; set; }

        public Stars Stars { get; set; }
    }
}

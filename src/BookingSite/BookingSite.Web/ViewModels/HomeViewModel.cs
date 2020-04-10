using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingSite.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<HotelDTO> Hotels { get; set; }

        public SortViewModel SortViewModel { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public FilterViewModel FilterViewModel { get; set; }
    }
}

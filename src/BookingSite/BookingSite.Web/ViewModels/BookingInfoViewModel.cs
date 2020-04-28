using BookingSite.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSite.Web.ViewModels
{
    public class BookingInfoViewModel
    {
        public IEnumerable<BookingDTO> Bookings { get; set; }
    }
}

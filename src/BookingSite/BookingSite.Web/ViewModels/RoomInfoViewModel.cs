using BookingSite.Data.Models;
using BookingSite.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSite.Web.ViewModels
{
    public class RoomInfoViewModel
    {
        public RoomDTO Room { get; set; }

        public IEnumerable<BookingDTO> Bookings { get; set; }
    }
}

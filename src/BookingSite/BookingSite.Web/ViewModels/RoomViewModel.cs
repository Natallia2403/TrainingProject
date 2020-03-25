using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;

namespace BookingSite.Web.ViewModels
{
    public class RoomViewModel
    {
        public IEnumerable<Hotel> Hotels { get; set; }

        public Room Room { get; set; }
    }
}

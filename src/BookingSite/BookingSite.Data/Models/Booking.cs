using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Data.Models
{
    public class Booking
    {
        public int Id { get; set; }
        
        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public int NumberOfGuests { get; set; }

        public List<Payment> Payments { get; set; }
    }
}

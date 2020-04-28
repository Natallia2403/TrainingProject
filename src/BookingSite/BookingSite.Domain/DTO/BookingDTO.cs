using BookingSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Domain.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int RoomId { get; set; }

        public RoomDTO Room { get; set; }
    }
}

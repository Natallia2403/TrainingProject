using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Domain.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public int RoomId { get; set; }

        public RoomDTO Room { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public List<PaymentDTO> Payments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Domain.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int MaxNumberOfGuests { get; set; }

        public int Price { get; set; }

        /// <summary>
        /// В процентах
        /// </summary>
        public int PrePayment { get; set; }

        public bool HasBalcony { get; set; }

        public bool HasKitchen { get; set; }

        public int HotelId { get; set; }

        public HotelDTO Hotel { get; set; }

        public List<BookingDTO> Bookings { get; set; }
    }
}

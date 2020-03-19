using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingProject.Data.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int MaxNumberOfGuests { get; set; }

        public decimal Price { get; set; }

        /// <summary>
        /// В процентах
        /// </summary>
        public int PrePayment { get; set; }

        public bool IsBalcony { get; set; }

        public bool IsKitchen { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}

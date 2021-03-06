﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Data.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int MaxNumberOfGuests { get; set; }

        public int Price { get; set; }

        public bool HasBalcony { get; set; }

        public bool HasKitchen { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}

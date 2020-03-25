using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Data.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public int Stars { get; set; }

        public bool IsAppartment { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public List<Gallery> Galleries { get; set; }

        public List<Room> Rooms { get; set; }
    }
}

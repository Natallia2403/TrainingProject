using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Domain.DTO
{
    public class GalleryDTO
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public int HotelId { get; set; }

        public HotelDTO Hotel { get; set; }
    }
}

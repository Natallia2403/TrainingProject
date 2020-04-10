using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingSite.Domain.DTO
{
    public class HotelDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public int Stars { get; set; }

        public bool IsAppartment { get; set; }

        public int CountryId { get; set; }

        public CountryDTO Country { get; set; }

        public List<GalleryDTO> Galleries { get; set; }

        public List<RoomDTO> Rooms { get; set; }
    }
}

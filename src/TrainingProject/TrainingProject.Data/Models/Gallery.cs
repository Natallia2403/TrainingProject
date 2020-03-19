using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingProject.Data.Models
{
    public class Gallery
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingProject.Data.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public List<Hotel> Hotels { get; set; }
    }
}

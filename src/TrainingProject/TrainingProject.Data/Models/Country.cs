using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingProject.Data.Models
{
    public class Country    
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Client> Clients { get; set; }

        public List<Hotel> Hotels { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Domain.DTO
{
    public class CountryDTO    
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ClientDTO> Clients { get; set; }

        public List<HotelDTO> Hotels { get; set; }
    }
}

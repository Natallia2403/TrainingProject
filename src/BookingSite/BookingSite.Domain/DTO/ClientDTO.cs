using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Domain.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int CountryId { get; set; }

        public CountryDTO Country { get; set; }

        public List<BookingDTO> Bookings { get; set; }
    }
}

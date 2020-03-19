using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingProject.Data.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}

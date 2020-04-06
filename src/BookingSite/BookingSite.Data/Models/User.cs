using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingSite.Data.Models
{
    public class User : IdentityUser
    {
        public string PasswordConfirm { get; set; }

        public int Year { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingProject.Data.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}

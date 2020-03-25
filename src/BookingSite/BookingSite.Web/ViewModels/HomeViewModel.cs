using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;

namespace BookingSite.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Hotel> Hotels { get; set; }
    }
}

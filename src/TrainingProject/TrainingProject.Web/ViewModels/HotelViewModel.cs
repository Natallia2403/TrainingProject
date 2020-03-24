using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingProject.Data.Models;

namespace TrainingProject.Web.ViewModels
{
    public class HotelViewModel
    {
        public IEnumerable<Country> Countries { get; set; }

        public Hotel Hotel { get; set; }
    }
}

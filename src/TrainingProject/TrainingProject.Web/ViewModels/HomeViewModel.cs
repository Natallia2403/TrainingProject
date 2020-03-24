using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingProject.Data.Models;

namespace TrainingProject.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Hotel> Hotels { get; set; }
    }
}

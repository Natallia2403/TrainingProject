using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingProject.Data.Models;

namespace TrainingProject.Web.Interfaces
{
    public interface ICountryManager
    {
        IEnumerable<Country> GetAll();
    }
}

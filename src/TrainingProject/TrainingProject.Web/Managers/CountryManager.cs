using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingProject.Data;
using TrainingProject.Data.Models;
using TrainingProject.Web.Interfaces;

namespace TrainingProject.Web.Managers
{
    public class CountryManager : ICountryManager
    {
        DataContext _dataContext;

        public CountryManager(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public IEnumerable<Country> GetAll()
        {
            return _dataContext.Countries.ToList();
        }
    }
}


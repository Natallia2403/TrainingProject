﻿using BookingSite.Domain.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSite.Web.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(List<CountryDTO> countries, int? country, string name, DateTime dateFrom, DateTime dateTo)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            countries.Insert(0, new CountryDTO { Name = "Все", Id = 0 });
            Countries = new SelectList(countries, "Id", "Name", country);
            SelectedCountry = country;
            SelectedName = name;
            SelectedDateFrom = dateFrom;
            SelectedDateTo = dateTo;
        }

        public SelectList Countries { get; private set; } 

        public int? SelectedCountry { get; private set; }  

        public string SelectedName { get; private set; }

        [DataType(DataType.Date)]
        public DateTime SelectedDateFrom { get; private set; }

        [DataType(DataType.Date)]
        public DateTime SelectedDateTo { get; private set; }
    }
}

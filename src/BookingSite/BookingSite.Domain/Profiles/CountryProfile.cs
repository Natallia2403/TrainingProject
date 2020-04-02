using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;

namespace BookingSite.Data.Profiles
{
    public class CountryProfile : Profile   
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            //CreateMap<CountryDTO, Country>();
        }
    }
}

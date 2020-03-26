using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;

namespace BookingSite.Web.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDTO>().ReverseMap();
        }
    }
}

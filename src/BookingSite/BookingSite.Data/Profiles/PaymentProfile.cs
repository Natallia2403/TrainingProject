using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BookingSite.Data.Models;
using BookingSite.Domain.DTO;

namespace BookingSite.Web.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDTO>().ReverseMap();
        }
    }
}

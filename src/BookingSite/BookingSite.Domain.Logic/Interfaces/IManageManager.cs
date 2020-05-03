using BookingSite.Domain.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Domain.Logic.Interfaces
{
    public interface IManageManager
    {
        IEnumerable<HotelDTO> GetHotelsByUserId(HttpContext httpContext);
    }
}

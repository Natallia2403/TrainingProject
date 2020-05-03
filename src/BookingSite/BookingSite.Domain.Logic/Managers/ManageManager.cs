using BookingSite.Domain.DTO;
using BookingSite.Domain.Interfaces;
using BookingSite.Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSite.Domain.Logic.Managers
{
    public class ManageManager: IManageManager
    {
        IHotelRepository _hotelRepository;
        IUserRepository _userRepository;

        public ManageManager(IHotelRepository hotelRepositary, IUserRepository userManager)
        {
            _hotelRepository = hotelRepositary ?? throw new ArgumentNullException(nameof(hotelRepositary));
            _userRepository = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public IEnumerable<HotelDTO> GetHotelsByUserId(HttpContext httpContext)
        {
            //var userName = _userRepository.GetCurrentUserName(HttpContext);
            //var user = _userRepository.FindByNameAsync(userName);
            //var userId = user.Id;

            var userId = _userRepository.GetCurrentUserId(httpContext);

            var dto = _hotelRepository.GetByUserIdAsync(userId);

            return dto.Result;
        }
    }
}

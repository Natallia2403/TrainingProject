using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;
using BookingSite.Domain.Interfaces;
using BookingSite.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookingSite.Web.Controllers
{
    public class BookingController : Controller
    {
        ILogger _logger;
        IHotelRepository _hotelRepository;
        ICountryRepository _countryRepository;
        IRoomRepository _roomRepository;
        private readonly UserManager<User> _userRepository;
        IBookingRepository _bookingRepository;

        public BookingController(ILogger<HomeController> logger,
                                 IHotelRepository hotelRepositary, ICountryRepository countryRepositary,
                                 IRoomRepository roomRepositary, UserManager<User> userManager, IBookingRepository bookingRepositary)
        {
            _logger = logger;
            _hotelRepository = hotelRepositary ?? throw new ArgumentNullException(nameof(hotelRepositary));
            _countryRepository = countryRepositary ?? throw new ArgumentNullException(nameof(countryRepositary));
            _roomRepository = roomRepositary ?? throw new ArgumentNullException(nameof(roomRepositary));
            _userRepository = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _bookingRepository = bookingRepositary ?? throw new ArgumentNullException(nameof(bookingRepositary));
        }

        public async Task<IActionResult> IndexAsync()
        {
            var userId = _userRepository.FindByNameAsync(HttpContext.User.Identity.Name).Result.Id;

            var bookings = await _bookingRepository.GetByUserId(userId);

            BookingInfoViewModel viewModel = new BookingInfoViewModel
            {
                Bookings = bookings
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> BookingDelete(int? id)
        {
            await _bookingRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
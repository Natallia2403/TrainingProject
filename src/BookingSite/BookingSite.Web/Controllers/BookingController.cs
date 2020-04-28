using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSite.Data.Models;
using BookingSite.Domain.Logic.Interfaces;
using BookingSite.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookingSite.Web.Controllers
{
    public class BookingController : Controller
    {
        ILogger _logger;
        IHotelManager _hotelManager;
        ICountryManager _countryManager;
        IRoomManager _roomManager;
        private readonly UserManager<User> _userManager;
        IBookingManager _bookingManager;

        public BookingController(ILogger<HomeController> logger,
                                 IHotelManager hotelManager, ICountryManager countryManager,
                                 IRoomManager roomManager, UserManager<User> userManager, IBookingManager bookingManager)
        {
            _logger = logger;
            _hotelManager = hotelManager ?? throw new ArgumentNullException(nameof(hotelManager));
            _countryManager = countryManager ?? throw new ArgumentNullException(nameof(countryManager));
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _bookingManager = bookingManager ?? throw new ArgumentNullException(nameof(bookingManager));
        }

        public async Task<IActionResult> IndexAsync()
        {
            var userId = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result.Id;

            var bookings = await _bookingManager.GetByUserId(userId);

            BookingInfoViewModel viewModel = new BookingInfoViewModel
            {
                Bookings = bookings
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> BookingDelete(int? id)
        {
            await _bookingManager.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
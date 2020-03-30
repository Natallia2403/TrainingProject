using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Domain.Logic.Interfaces;
using BookingSite.Web.ViewModels;
using BookingSite.Domain.DTO;
using Microsoft.AspNetCore.Identity;

namespace BookingSite.Web.Controllers
{
    public class HomeController : Controller
    {
        ILogger _logger;
        IHotelManager _hotelManager;
        IRoomManager _roomManager;
        IBookingManager _bookingManager;
        UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger,
                                 IHotelManager hotelManager, IRoomManager roomManager, IBookingManager bookingManager,
                                 UserManager<User> userManager)
        {
            _logger = logger;
            _hotelManager = hotelManager ?? throw new ArgumentNullException(nameof(hotelManager));
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));
            _bookingManager = bookingManager ?? throw new ArgumentNullException(nameof(bookingManager));
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var dto = await _hotelManager.GetAllAsync();

            HomeViewModel hvm = new HomeViewModel { Hotels = dto };

            return View(hvm);
        }

        public async Task<IActionResult> HotelDetails(int? id)
        {
            var dto = await _hotelManager.GetByIdAsync(id);

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> RoomDetails(int? id)
        {
            var dto = await _roomManager.GetByIdAsync(id);

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Book(int? id)
        {
            var userName = HttpContext.User.Identity.Name;

            var user = _userManager.FindByNameAsync(userName);

            var dto = new BookingDTO { RoomId = id.Value, UserId = user.Id };

            await _bookingManager.AddAsync(dto);

            return RedirectToAction("Index");
        }
    }
}
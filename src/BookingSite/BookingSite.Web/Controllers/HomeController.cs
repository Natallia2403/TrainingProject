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

        public HomeController(ILogger<HomeController> logger,
                                 IHotelManager hotelManager, IRoomManager roomManager, IBookingManager bookingManager)
        {
            _logger = logger;
            _hotelManager = hotelManager ?? throw new ArgumentNullException(nameof(hotelManager));
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));
            _bookingManager = bookingManager ?? throw new ArgumentNullException(nameof(bookingManager));
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

        [HttpPost]
        public async Task<IActionResult> Book(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            var userName = HttpContext.User.Identity.Name;

            var dto = new BookingDTO { RoomId = id.Value, UserName = userName };

            await _bookingManager.AddAsync(dto);

            return RedirectToAction("Index");
        }
    }
}
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

namespace BookingSite.Web.Controllers
{
    public class HomeController : Controller
    {
        ILogger _logger;
        IHotelManager _hotelManager;
        IRoomManager _roomManager;

        public HomeController(ILogger<HomeController> logger,
                                 IHotelManager hotelManager, IRoomManager roomManager)
        {
            _logger = logger;
            _hotelManager = hotelManager ?? throw new ArgumentNullException(nameof(hotelManager));
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));
        }

        public async Task<IActionResult> IndexAsync()
        {
            var dto = await _hotelManager.GetAllAsync();

            HomeViewModel hvm = new HomeViewModel { Hotels = dto };

            return View(hvm);
        }

        public async Task<IActionResult> HotelDetailsAsync(int? id)
        {
            var dto = await _hotelManager.GetByIdAsync(id);

            return View(dto);
        }

        public async Task<IActionResult> RoomDetailsAsync(int? id)
        {
            var dto = await _roomManager.GetByIdAsync(id);

            return View(dto);
        }
    }
}
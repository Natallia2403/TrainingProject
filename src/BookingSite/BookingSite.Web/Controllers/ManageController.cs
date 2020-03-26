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
using Microsoft.AspNetCore.Authorization;

namespace BookingSite.Web.Controllers
{
    [Authorize(Roles = "manager")]
    public class ManageController : Controller
    {
        ILogger _logger;
        IHotelManager _hotelManager;
        ICountryManager _countryManager;
        IRoomManager _roomManager;

        public ManageController(ILogger<HomeController> logger,
                                 IHotelManager hotelManager, ICountryManager countryManager, IRoomManager roomManager)
        {
            _logger = logger;
            _hotelManager = hotelManager ?? throw new ArgumentNullException(nameof(hotelManager));
            _countryManager = countryManager ?? throw new ArgumentNullException(nameof(countryManager));
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));
        }

        public async Task<IActionResult> IndexAsync()
        {
            var hotels = await _hotelManager.GetAllAsync();

            HomeViewModel hvm = new HomeViewModel { Hotels = hotels };

            return View(hvm);
        }

        [HttpGet]
        public async Task<IActionResult> HotelCreateAsync()
        {
            var countries = await _countryManager.GetAllAsync();

            HotelViewModel chvm = new HotelViewModel { Countries = countries };

            return View(chvm);
        }

        [HttpPost]
        public async Task<IActionResult> HotelCreate(Hotel hotel)
        {
            await _hotelManager.AddAsync(hotel);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> HotelDetailsAsync(int? id)
        {
            var hotel = await _hotelManager.GetByIdAsync(id);

            return View(hotel);
        }

        public async Task<IActionResult> HotelEditAsync(int? id)
        {
            Hotel hotel = await _hotelManager.GetByIdAsync(id);

            var countries = await _countryManager.GetAllAsync();

            HotelViewModel hvm = new HotelViewModel { Countries = countries, Hotel = hotel };

            return View(hvm);
        }

        [HttpPost]
        public async Task<IActionResult> HotelEdit(HotelViewModel hotelViewModel)
        {
            await _hotelManager.UpdateAsync(hotelViewModel.Hotel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> HotelDelete(int? id)
        {
            var hotel = await _hotelManager.GetByIdAsync(id);

            if (hotel.Rooms != null && hotel.Rooms.Count > 0)
            {
                throw new Exception("Пожалуйста, сначала удалите комнаты");
            }

            await _hotelManager.DeleteAsync(hotel);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RoomDetailsAsync(int? id)
        {
            Room room = await _roomManager.GetByIdAsync(id);

            return View(room);
        }

        public IActionResult RoomCreate(int? roomId)
        {
            ViewBag.HotelId = roomId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoomCreate(Room room)
        {
            await _roomManager.AddAsync(room);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RoomEditAsync(int? id)
        {
            Room room = await _roomManager.GetByIdAsync(id);

            var hotels = await _hotelManager.GetAllAsync();

            RoomViewModel rvm = new RoomViewModel { Hotels = hotels, Room = room };

            return View(rvm);
        }

        [HttpPost]
        public async Task<IActionResult> RoomEdit(RoomViewModel roomViewModel)
        {
            await _roomManager.UpdateAsync(roomViewModel.Room);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RoomDelete(int? id)
        {
            await _roomManager.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
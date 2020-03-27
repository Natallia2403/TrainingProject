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
using BookingSite.Domain.DTO;

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

        public async Task<IActionResult> Index()
        {
            var dto = await _hotelManager.GetAllAsync();

            HomeViewModel hvm = new HomeViewModel { Hotels = dto };

            return View(hvm);
        }

        [HttpGet]
        public async Task<IActionResult> HotelCreate()
        {
            var dto = await _countryManager.GetAllAsync();

            HotelViewModel viewModel = new HotelViewModel { Countries = dto };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> HotelCreate(HotelViewModel viewModel)
        {
            var dto = viewModel.Hotel;

            await _hotelManager.AddAsync(dto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> HotelDetails(int? id)
        {
            var dto = await _hotelManager.GetByIdAsync(id);

            return View(dto);
        }

        public async Task<IActionResult> HotelEdit(int? id)
        {
            var dto = await _hotelManager.GetByIdAsync(id);

            var countries = await _countryManager.GetAllAsync();

            HotelViewModel hvm = new HotelViewModel { Countries = countries, Hotel = dto };

            return View(hvm);
        }

        [HttpPost]
        public async Task<IActionResult> HotelEdit(HotelViewModel viewModel)
        {
            var dto = viewModel.Hotel;

            await _hotelManager.UpdateAsync(dto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> HotelDelete(int? id)
        {
            var dto = await _hotelManager.GetByIdAsync(id);

            if (dto.Rooms != null && dto.Rooms.Count > 0)
            {
                throw new Exception("Пожалуйста, сначала удалите комнаты");
            }

            await _hotelManager.DeleteAsync(dto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RoomDetails(int? id)
        {
            var dto = await _roomManager.GetByIdAsync(id);

            return View(dto);
        }

        public IActionResult RoomCreate(int? hotelId)
        {
            ViewBag.HotelId = hotelId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoomCreate(RoomDTO dto)
        {
            await _roomManager.AddAsync(dto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RoomEdit(int? id)
        {
            var dtoRoom = await _roomManager.GetByIdAsync(id);

            var dtoHotels = await _hotelManager.GetAllAsync();

            RoomViewModel viewModel = new RoomViewModel { Hotels = dtoHotels, Room = dtoRoom };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RoomEdit(RoomViewModel viewModel)
        {
            var dto = viewModel.Room;

            await _roomManager.UpdateAsync(dto);

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Web.Interfaces;
using BookingSite.Web.ViewModels;

namespace BookingSite.Web.Controllers
{
    public class HomeController : Controller
    {
        DataContext _db;
        private readonly ILogger _logger;

        IHotelManager _hotelManager;
        ICountryManager _countryManager;
        IRoomManager _roomManager;

        public HomeController(DataContext context,
                                ILogger<HomeController> logger,
                                 IHotelManager hotelManager, ICountryManager countryManager, IRoomManager roomManager)
        {
            _logger = logger;

            context = context ?? throw new ArgumentNullException(nameof(context));
            _hotelManager = hotelManager ?? throw new ArgumentNullException(nameof(hotelManager));
            _countryManager = countryManager ?? throw new ArgumentNullException(nameof(countryManager));
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));

            _db = context;
        }

        public IActionResult Index()
        {
            List<Hotel> hotels = _hotelManager.GetAll().ToList();

            HomeViewModel hvm = new HomeViewModel { Hotels = hotels };

            return View(hvm);
        }

        [HttpGet]
        public IActionResult HotelCreate()
        {
            IEnumerable<Country> countries = _countryManager.GetAll();

            HotelViewModel chvm = new HotelViewModel { Countries = countries };

            return View(chvm);
        }

        [HttpPost]
        public async Task<IActionResult> HotelCreate(Hotel hotel)
        {
            await _hotelManager.AddAsync(hotel);

            return RedirectToAction("Index");
        }

        public IActionResult HotelDetails(int? id)
        {
            Hotel hotel = _hotelManager.GetById(id);

            return View(hotel);
        }

        public IActionResult HotelEdit(int? id)
        {
            Hotel hotel = _hotelManager.GetById(id);

            IEnumerable<Country> countries = _countryManager.GetAll();

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
            Hotel hotel = _hotelManager.GetById(id);

            if (hotel.Rooms != null && hotel.Rooms.Count > 0)
            {
                throw new Exception("Пожалуйста, сначала удалите комнаты");
            }

            await _hotelManager.DeleteAsync(hotel);

            return RedirectToAction("Index");
        }

        public IActionResult RoomDetails(int? id)
        {
            Room room = _roomManager.GetById(id);

            return View(room);
        }

        public IActionResult RoomCreate(int? PhoneId)
        {
            ViewBag.HotelId = PhoneId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoomCreate(Room room)
        {
            await _roomManager.AddAsync(room);

            return RedirectToAction("Index");
        }

        public IActionResult RoomEdit(int? id)
        {
            Room room = _roomManager.GetById(id);

            IEnumerable<Hotel> hotels = _hotelManager.GetAll();

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
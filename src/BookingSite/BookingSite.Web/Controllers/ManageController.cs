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
using Microsoft.AspNetCore.Identity;

namespace BookingSite.Web.Controllers
{
    [Authorize(Roles = "manager")]
    public class ManageController : Controller
    {
        ILogger _logger;
        IHotelManager _hotelManager;
        ICountryManager _countryManager;
        IRoomManager _roomManager;
        private readonly UserManager<User> _userManager;
        IBookingManager _bookingManager;

        //public ManageController(IHotelManager hotelManager)
        //{
        //    _hotelManager = hotelManager ?? throw new ArgumentNullException(nameof(hotelManager));
        //}

        public ManageController(ILogger<HomeController> logger,
                                 IHotelManager hotelManager, ICountryManager countryManager, 
                                 IRoomManager roomManager, UserManager<User> userManager, 
                                 IBookingManager bookingManager)
        {
            _logger = logger;
            _hotelManager = hotelManager ?? throw new ArgumentNullException(nameof(hotelManager));
            _countryManager = countryManager ?? throw new ArgumentNullException(nameof(countryManager));
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _bookingManager = bookingManager ?? throw new ArgumentNullException(nameof(bookingManager));
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result.Id;

            var dto = await _hotelManager.GetByUserIdAsync(userId);

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
            if (ModelState.IsValid)
            {
                var userId = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result.Id;

                var dto = new HotelDTO { UserId = userId, Name = viewModel.Name, Description = viewModel.Description, Address = viewModel.Address, CountryId = viewModel.CountryId, IsAppartment = viewModel.IsAppartment, Stars = viewModel.Stars };

                await _hotelManager.AddAsync(dto);

                return RedirectToAction("Index");
            }
            else
            {
                var countries = await _countryManager.GetAllAsync();

                viewModel.Countries = countries;

                return View(viewModel);
            }
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

            HotelViewModel hvm = new HotelViewModel { Countries = countries, Id = dto.Id, Name = dto.Name, Description = dto.Description, Address = dto.Address, CountryId = dto.CountryId, IsAppartment = dto.IsAppartment, Stars = dto.Stars };

            return View(hvm);
        }

        [HttpPost]
        public async Task<IActionResult> HotelEdit(HotelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result.Id;

                var dto = new HotelDTO { Id = viewModel.Id, Name = viewModel.Name, Description = viewModel.Description, Address = viewModel.Address, CountryId = viewModel.CountryId, IsAppartment = viewModel.IsAppartment, Stars = viewModel.Stars, UserId = userId };

                await _hotelManager.UpdateAsync(dto);

                return RedirectToAction("Index");
            }
            else
            {
                var countries = await _countryManager.GetAllAsync();

                viewModel.Countries = countries;

                return View(viewModel);
            }
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

            var bookings = await _bookingManager.GetByRoomIdAsync(id.Value);

            RoomInfoViewModel viewModel = new RoomInfoViewModel
            {
                Room = dto,
                Bookings = bookings
            };

            return View(viewModel);
        }

        public async Task<IActionResult> RoomCreateAsync(int? hotelId)
        {
            var dtoHotels = await _hotelManager.GetAllAsync();

            RoomViewModel viewModel = new RoomViewModel { Hotels = dtoHotels, HotelId = hotelId.Value };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RoomCreate(RoomViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dto = new RoomDTO { Description = viewModel.Description, MaxNumberOfGuests = viewModel.MaxNumberOfGuests.Value, Price = viewModel.Price.Value, HasBalcony = viewModel.HasBalcony, HasKitchen = viewModel.HasKitchen, HotelId = viewModel.HotelId };

                await _roomManager.AddAsync(dto);

                return RedirectToAction("Index");
            }
            else
            {
                var dtoHotels = await _hotelManager.GetAllAsync();

                viewModel.Hotels = dtoHotels;

                ViewBag.HotelId = viewModel.HotelId;

                return View(viewModel);
            }
        }

        public async Task<IActionResult> RoomEdit(int? id)
        {
            var dtoRoom = await _roomManager.GetByIdAsync(id);

            var dtoHotels = await _hotelManager.GetAllAsync();

            RoomViewModel viewModel = new RoomViewModel { Hotels = dtoHotels, Description = dtoRoom.Description, MaxNumberOfGuests = dtoRoom.MaxNumberOfGuests, Price = dtoRoom.Price, HasBalcony = dtoRoom.HasBalcony, HasKitchen = dtoRoom.HasKitchen, HotelId = dtoRoom.HotelId, Id = dtoRoom.Id };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RoomEdit(RoomViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dto = new RoomDTO { Description = viewModel.Description, MaxNumberOfGuests = viewModel.MaxNumberOfGuests.Value, Price = viewModel.Price.Value, HasBalcony = viewModel.HasBalcony, HasKitchen = viewModel.HasKitchen, HotelId = viewModel.HotelId, Id = viewModel.Id };

                await _roomManager.UpdateAsync(dto);

                return RedirectToAction("Index");
            }
            else
            {
                var dtoHotels = await _hotelManager.GetAllAsync();

                viewModel.Hotels = dtoHotels;

                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RoomDelete(int? id)
        {
            await _roomManager.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckHotelNameAsync(string name)
        {
            var hotels = await _hotelManager.GetAllAsync();

            foreach(var h in hotels)
            {
                if(h.Name == name)
                    return Json(false);//Данные не прошли валидацию
            }
            return Json(true);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Domain.Interfaces;
using BookingSite.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BookingSite.Domain.DTO;
using Microsoft.AspNetCore.Identity;
using BookingSite.Domain.Logic.Interfaces;

namespace BookingSite.Web.Controllers
{
    [Authorize(Roles = "manager")]
    public class ManageController : Controller
    {
        ILogger _logger;
        IHotelRepository _hotelRepository;
        ICountryRepository _countryRepository;
        IRoomRepository _roomRepository;
        IUserRepository _userRepository;
        IBookingRepository _bookingRepository;

        IManageManager _manageManager;

        public ManageController(ILogger<HomeController> logger,
                                 IHotelRepository hotelRepositary, ICountryRepository countryRepositary,
                                 IRoomRepository roomRepositary, IUserRepository userManager,
                                 IBookingRepository bookingRepositary, IManageManager manageManager)
        {
            _logger = logger;
            _hotelRepository = hotelRepositary ?? throw new ArgumentNullException(nameof(hotelRepositary));
            _countryRepository = countryRepositary ?? throw new ArgumentNullException(nameof(countryRepositary));
            _roomRepository = roomRepositary ?? throw new ArgumentNullException(nameof(roomRepositary));
            _userRepository = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _bookingRepository = bookingRepositary ?? throw new ArgumentNullException(nameof(bookingRepositary));

            _manageManager = manageManager ?? throw new ArgumentNullException(nameof(manageManager));
        }

        [Authorize]
        public IActionResult Index()
        {
            var hotelsDTO = _manageManager.GetHotelsByUserId(HttpContext);

            HomeViewModel hvm = new HomeViewModel { Hotels = hotelsDTO };

            return View(hvm);
        }

        [HttpGet]
        public async Task<IActionResult> HotelCreate()
        {
            var dto = await _countryRepository.GetAllAsync();

            HotelViewModel viewModel = new HotelViewModel { Countries = dto };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> HotelCreate(HotelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _userRepository.FindByNameAsync(HttpContext.User.Identity.Name).Id;

                var dto = new HotelDTO { UserId = userId, Name = viewModel.Name, Description = viewModel.Description, Address = viewModel.Address, CountryId = viewModel.CountryId, IsAppartment = viewModel.IsAppartment, Stars = viewModel.Stars };

                await _hotelRepository.AddAsync(dto);

                return RedirectToAction("Index");
            }
            else
            {
                var countries = await _countryRepository.GetAllAsync();

                viewModel.Countries = countries;

                return View(viewModel);
            }
        }

        public async Task<IActionResult> HotelDetails(int? id)
        {
            var dto = await _hotelRepository.GetByIdAsync(id);

            return View(dto);
        }

        public async Task<IActionResult> HotelEdit(int? id)
        {
            var dto = await _hotelRepository.GetByIdAsync(id);

            var countries = await _countryRepository.GetAllAsync();

            HotelViewModel hvm = new HotelViewModel { Countries = countries, Id = dto.Id, Name = dto.Name, Description = dto.Description, Address = dto.Address, CountryId = dto.CountryId, IsAppartment = dto.IsAppartment, Stars = dto.Stars };

            return View(hvm);
        }

        [HttpPost]
        public async Task<IActionResult> HotelEdit(HotelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _userRepository.FindByNameAsync(HttpContext.User.Identity.Name).Id;

                var dto = new HotelDTO { Id = viewModel.Id, Name = viewModel.Name, Description = viewModel.Description, Address = viewModel.Address, CountryId = viewModel.CountryId, IsAppartment = viewModel.IsAppartment, Stars = viewModel.Stars, UserId = userId };

                await _hotelRepository.UpdateAsync(dto);

                return RedirectToAction("Index");
            }
            else
            {
                var countries = await _countryRepository.GetAllAsync();

                viewModel.Countries = countries;

                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> HotelDelete(int? id)
        {
            var dto = await _hotelRepository.GetByIdAsync(id);

            if (dto.Rooms != null && dto.Rooms.Count > 0)
            {
                throw new Exception("Пожалуйста, сначала удалите комнаты");
            }

            await _hotelRepository.DeleteAsync(dto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RoomDetails(int? id)
        {
            var dto = await _roomRepository.GetByIdAsync(id);

            var bookings = await _bookingRepository.GetByRoomIdAsync(id.Value);

            RoomInfoViewModel viewModel = new RoomInfoViewModel
            {
                Room = dto,
                Bookings = bookings
            };

            return View(viewModel);
        }

        public async Task<IActionResult> RoomCreateAsync(int? hotelId)
        {
            var dtoHotels = await _hotelRepository.GetAllAsync();

            RoomViewModel viewModel = new RoomViewModel { Hotels = dtoHotels, HotelId = hotelId.Value };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RoomCreate(RoomViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dto = new RoomDTO { Description = viewModel.Description, MaxNumberOfGuests = viewModel.MaxNumberOfGuests.Value, Price = viewModel.Price.Value, HasBalcony = viewModel.HasBalcony, HasKitchen = viewModel.HasKitchen, HotelId = viewModel.HotelId };

                await _roomRepository.AddAsync(dto);

                return RedirectToAction("Index");
            }
            else
            {
                var dtoHotels = await _hotelRepository.GetAllAsync();

                viewModel.Hotels = dtoHotels;

                ViewBag.HotelId = viewModel.HotelId;

                return View(viewModel);
            }
        }

        public async Task<IActionResult> RoomEdit(int? id)
        {
            var dtoRoom = await _roomRepository.GetByIdAsync(id);

            var dtoHotels = await _hotelRepository.GetAllAsync();

            RoomViewModel viewModel = new RoomViewModel { Hotels = dtoHotels, Description = dtoRoom.Description, MaxNumberOfGuests = dtoRoom.MaxNumberOfGuests, Price = dtoRoom.Price, HasBalcony = dtoRoom.HasBalcony, HasKitchen = dtoRoom.HasKitchen, HotelId = dtoRoom.HotelId, Id = dtoRoom.Id };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RoomEdit(RoomViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dto = new RoomDTO { Description = viewModel.Description, MaxNumberOfGuests = viewModel.MaxNumberOfGuests.Value, Price = viewModel.Price.Value, HasBalcony = viewModel.HasBalcony, HasKitchen = viewModel.HasKitchen, HotelId = viewModel.HotelId, Id = viewModel.Id };

                await _roomRepository.UpdateAsync(dto);

                return RedirectToAction("Index");
            }
            else
            {
                var dtoHotels = await _hotelRepository.GetAllAsync();

                viewModel.Hotels = dtoHotels;

                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RoomDelete(int? id)
        {
            await _roomRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckHotelNameAsync(string name)
        {
            var hotels = await _hotelRepository.GetAllAsync();

            foreach (var h in hotels)
            {
                if (h.Name == name)
                    return Json(false);//Данные не прошли валидацию
            }
            return Json(true);
        }
    }
}
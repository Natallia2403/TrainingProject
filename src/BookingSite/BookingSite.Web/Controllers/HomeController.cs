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
using BookingSite.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace BookingSite.Web.Controllers
{
    public class HomeController : Controller
    {
        ILogger _logger;
        IHotelManager _hotelManager;
        IRoomManager _roomManager;
        IBookingManager _bookingManager;
        ICountryManager _countryManager;

        public HomeController(ILogger<HomeController> logger,
                                 IHotelManager hotelManager, IRoomManager roomManager, 
                                 IBookingManager bookingManager, ICountryManager countryManager)
        {
            _logger = logger;
            _hotelManager = hotelManager ?? throw new ArgumentNullException(nameof(hotelManager));
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));
            _bookingManager = bookingManager ?? throw new ArgumentNullException(nameof(bookingManager));
            _countryManager = countryManager ?? throw new ArgumentNullException(nameof(countryManager));
        }

        public async Task<IActionResult> Index(int? country, string name, DateTime dateFrom, DateTime dateTo, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 3;

            //фильтрация
            var hotelsDto = await _hotelManager.GetAllAsync();
            if (country != null && country != 0)
            {
                hotelsDto = hotelsDto.Where(p => p.CountryId == country);
            }
            if (!String.IsNullOrEmpty(name))
            {
                hotelsDto = hotelsDto.Where(p => p.Name.Contains(name));
            }
            //TODO: здесь фильтрация по датам

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    hotelsDto = hotelsDto.OrderByDescending(s => s.Name);
                    break;
                case SortState.StarsAsc:
                    hotelsDto = hotelsDto.OrderBy(s => s.Stars);
                    break;
                case SortState.StarsDesc:
                    hotelsDto = hotelsDto.OrderByDescending(s => s.Stars);
                    break;
                case SortState.CountryAsc:
                    hotelsDto = hotelsDto.OrderBy(s => s.Country.Name);
                    break;
                case SortState.CountryDesc:
                    hotelsDto = hotelsDto.OrderByDescending(s => s.Country.Name);
                    break;
                default:
                    hotelsDto = hotelsDto.OrderBy(s => s.Name);
                    break;
            }


            // пагинация
            var HotelDTOLst = hotelsDto.ToList();
            var count = HotelDTOLst.Count();
            var items = HotelDTOLst.Skip((page - 1) * pageSize).Take(pageSize);

            //страны
            var countriesDto = await _countryManager.GetAllAsync();
            List<CountryDTO> countriesLst = countriesDto.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            CountryDTO allCountriesItem = new CountryDTO { Id = 0, Name = "Все" };
            countriesLst.Insert(0, allCountriesItem);

            // формируем модель представления
            HomeViewModel viewModel = new HomeViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(countriesLst, country, name, dateFrom, dateTo),
                Hotels = items
            };

            return View(viewModel);
            //return RedirectToAction("SearchResult", viewModel);
        }

        public async Task<IActionResult> SearchResult(int? country, string name, DateTime dateFrom, DateTime dateTo, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            //фильтрация
            if (dateFrom == DateTime.MinValue || dateTo == DateTime.MinValue)
            {
                
            }

            var hotelsDto = await _hotelManager.GetAllAsync();

            if (country != null && country != 0)
            {
                hotelsDto = hotelsDto.Where(p => p.CountryId == country);
            }
            if (!String.IsNullOrEmpty(name))
            {
                hotelsDto = hotelsDto.Where(p => p.Name.Contains(name));
            }
            //TODO: здесь фильтрация по датам

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    hotelsDto = hotelsDto.OrderByDescending(s => s.Name);
                    break;
                case SortState.StarsAsc:
                    hotelsDto = hotelsDto.OrderBy(s => s.Stars);
                    break;
                case SortState.StarsDesc:
                    hotelsDto = hotelsDto.OrderByDescending(s => s.Stars);
                    break;
                case SortState.CountryAsc:
                    hotelsDto = hotelsDto.OrderBy(s => s.Country.Name);
                    break;
                case SortState.CountryDesc:
                    hotelsDto = hotelsDto.OrderByDescending(s => s.Country.Name);
                    break;
                default:
                    hotelsDto = hotelsDto.OrderBy(s => s.Name);
                    break;
            }


            // пагинация
            int pageSize = 3;
            var HotelDTOLst = hotelsDto.ToList();
            var count = HotelDTOLst.Count();
            var items = HotelDTOLst.Skip((page - 1) * pageSize).Take(pageSize);

            //страны
            var countriesDto = await _countryManager.GetAllAsync();
            List<CountryDTO> countriesLst = countriesDto.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            CountryDTO allCountriesItem = new CountryDTO { Id = 0, Name = "Все" };
            countriesLst.Insert(0, allCountriesItem);

            // формируем модель представления
            HomeViewModel viewModel = new HomeViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(countriesLst, country, name, dateFrom, dateTo),
                Hotels = items
            };

            return View(viewModel);
        }

        public async Task<IActionResult> HotelDetails(int? id, DateTime dateFrom, DateTime dateTo)
        {
            var hotelDto = await _hotelManager.GetByIdAsync(id);

            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;

            foreach (var room in hotelDto.Rooms)
                room.IsCanBeBooked = _bookingManager.IsCanBeBooked(room.Id, dateFrom, dateTo);

            return View(hotelDto);
        }

        [HttpGet]
        public async Task<IActionResult> RoomDetails(int? id, DateTime dateFrom, DateTime dateTo)
        {
            var dto = await _roomManager.GetByIdAsync(id);

            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;

            var isCanBeBooked = _bookingManager.IsCanBeBooked(id, dateFrom, dateTo);
            ViewBag.IsCanBeBooked = isCanBeBooked;

            return View(dto);
        }

        [HttpGet]
        [Authorize]
        [ActionName("Book")]
        public async Task<IActionResult> BookRoom(int? id, DateTime dateFrom, DateTime dateTo)
        {
            var result = MakeBook(id, dateFrom, dateTo);

            return await result;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Book(int? id, DateTime dateFrom, DateTime dateTo)
        {
            var result = MakeBook(id, dateFrom, dateTo);

            return await result;
        }

        private async Task<IActionResult> MakeBook(int? id, DateTime dateFrom, DateTime dateTo)
        {
            var userName = HttpContext.User.Identity.Name;

            var dto = new BookingDTO { RoomId = id.Value, UserName = userName, DateFrom = dateFrom, DateTo = dateTo };

            await _bookingManager.AddAsync(dto);

            return RedirectToAction("Index");
        }
    }
}
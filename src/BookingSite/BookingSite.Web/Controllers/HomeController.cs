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
using System.Globalization;

namespace BookingSite.Web.Controllers
{
    public class HomeController : Controller
    {
        ILogger _logger;
        IHotelManager _hotelManager;
        IRoomManager _roomManager;
        IBookingManager _bookingManager;
        ICountryManager _countryManager;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger,
                                 IHotelManager hotelManager, IRoomManager roomManager, 
                                 IBookingManager bookingManager, ICountryManager countryManager,
                                 UserManager<User> userManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hotelManager = hotelManager ?? throw new ArgumentNullException(nameof(hotelManager));
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));
            _bookingManager = bookingManager ?? throw new ArgumentNullException(nameof(bookingManager));
            _countryManager = countryManager ?? throw new ArgumentNullException(nameof(countryManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IActionResult> Index(int? country, string name, DateTime dateFrom, DateTime dateTo, 
            bool isRequired, bool isNotFuture, bool isNotCompare)
        {
            var aa = CultureInfo.CurrentCulture.Name;
            var bb = CultureInfo.CurrentUICulture.Name;

            //страны
            var countriesDto = await _countryManager.GetAllAsync();
            List<CountryDTO> countriesLst = countriesDto.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            CountryDTO allCountriesItem = new CountryDTO { Id = 0, Name = "Все" };
            countriesLst.Insert(0, allCountriesItem);

            if (isRequired)
                ViewBag.IsRequiredDate = string.Empty;
            else
                ViewBag.IsRequiredDate = "field-validation-valid";

            if (isNotFuture)
                ViewBag.IsNotFutureDate = string.Empty;
            else
                ViewBag.IsNotFutureDate = "field-validation-valid";

            if (isNotCompare)
                ViewBag.IsNotCopmpareDate = string.Empty;
            else
                ViewBag.IsNotCopmpareDate = "field-validation-valid";

            // формируем модель представления
            var viewModel = new FilterViewModel(countriesLst, country, name, dateFrom, dateTo);

            return View(viewModel);
        }

        public async Task<IActionResult> SearchResult(int? country, string name, DateTime dateFrom, DateTime dateTo,
            bool? isSort,
            SortState? sortOrder,
            int? page)
        {
            
            if (dateFrom == DateTime.MinValue || dateTo == DateTime.MinValue)//required validation
                return RedirectToAction("Index", new { country = country, name = name, dateFrom = dateFrom, dateTo = dateTo, isRequired = true });

            if (dateFrom > dateTo || dateFrom < DateTime.Now || dateTo < DateTime.Now)
            {
                var isNotCompare = false;
                var isNotFuture = false;

                if (dateFrom > dateTo)//compare validation
                    isNotCompare = true;

                if (dateFrom < DateTime.Now || dateTo < DateTime.Now)//future validation
                    isNotFuture = true;

                return RedirectToAction("Index", new { country = country, name = name, dateFrom = dateFrom, dateTo = dateTo, isNotFuture = isNotFuture, isNotCompare = isNotCompare });
            }

            //фильтрация
            var isFiltered = country.HasValue || !string.IsNullOrWhiteSpace(name);

            ViewBag.IsFiltered = isFiltered;

            var hotelsDto = await _hotelManager.GetAllAsync();

            if (country != null && country != 0)
                hotelsDto = hotelsDto.Where(p => p.CountryId == country);

            if (!String.IsNullOrEmpty(name))
                hotelsDto = hotelsDto.Where(p => p.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));

            // сортировка
            var sortOrderTemp = sortOrder.HasValue ? sortOrder.Value : SortState.NameAsc;
            var sortOrderFin = sortOrderTemp;
            if (!isSort.HasValue)
                sortOrderFin = new SortViewModel(sortOrderTemp).Current;
            var sortViewModel = new SortViewModel(sortOrderFin);

            var so = sortViewModel.Current;

            switch (so)
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

            var pageTemp = page.HasValue ? page.Value : 1;
            var items = HotelDTOLst.Skip((pageTemp - 1) * pageSize).Take(pageSize);

            //страны
            var countriesDto = await _countryManager.GetAllAsync();
            List<CountryDTO> countriesLst = countriesDto.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            CountryDTO allCountriesItem = new CountryDTO { Id = 0, Name = "Все" };
            countriesLst.Insert(0, allCountriesItem);

            // формируем модель представления
            HomeViewModel viewModel = new HomeViewModel
            {
                PageViewModel = new PageViewModel(count, pageTemp, pageSize),
                SortViewModel = sortViewModel,
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
            {
                var isCanBeBooked = _bookingManager.IsCanBeBookedAsync(room.Id, dateFrom, dateTo).Result;
                room.IsCanBeBooked = isCanBeBooked;
            }

            return View(hotelDto);
        }

        [HttpGet]
        public async Task<IActionResult> RoomDetails(int? id, DateTime dateFrom, DateTime dateTo)
        {
            var dto = await _roomManager.GetByIdAsync(id);

            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;

            var isCanBeBooked = await _bookingManager.IsCanBeBookedAsync(id, dateFrom, dateTo);
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
            var userId = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result.Id;

            var dto = new BookingDTO { RoomId = id.Value, UserId = userId, DateFrom = dateFrom, DateTo = dateTo };

            await _bookingManager.AddAsync(dto);

            return RedirectToAction("Index", "Booking");
        }
    }
}
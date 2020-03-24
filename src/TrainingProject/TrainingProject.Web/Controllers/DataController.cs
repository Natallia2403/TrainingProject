using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingProject.Data;
using TrainingProject.Data.Models;
using TrainingProject.Web.Interfaces;
using TrainingProject.Web.ViewModels;

namespace TrainingProject.Web.Controllers
{
    public class DataController : Controller
    {
        DataContext db;
        IHotelManager _hotelManager;
        ICountryManager _countryManager;
        IRoomManager _roomManager;

        public DataController(DataContext context, IHotelManager hotelManager, ICountryManager countryManager, IRoomManager roomManager)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            _hotelManager = hotelManager ?? throw new ArgumentNullException(nameof(hotelManager));
            _countryManager = countryManager ?? throw new ArgumentNullException(nameof(countryManager));
            _roomManager = roomManager ?? throw new ArgumentNullException(nameof(roomManager));

            db = context;
        }

        public IActionResult Add()
        {
            //Countries
            Country country1 = new Country { Name = "Беларусь" };
            Country country2 = new Country { Name = "Россия" };
            Country country3 = new Country { Name = "Украина" };
            Country country4 = new Country { Name = "Литва" };
            Country country5 = new Country { Name = "Швеция" };
            Country country6 = new Country { Name = "Финляндия" };

            db.Countries.Add(country1);
            db.Countries.Add(country2);
            db.Countries.Add(country3);
            db.Countries.Add(country4);
            db.Countries.Add(country5);
            db.Countries.Add(country6);

            //Clients
            Client client1 = new Client { FirstName = "Иван", LastName = "Иванов", Login = "ivan", Password = "123", Email = "n.kliuchnikova@sam-solutions.com", Country = country1 };
            Client client2 = new Client { FirstName = "Пётр", LastName = "Петров", Login = "petr", Password = "123", Email = "n.kliuchnikova@sam-solutions.com", Country = country2 };
            Client client3 = new Client { FirstName = "Сидор", LastName = "Сидоров", Login = "sidor", Password = "123", Email = "n.kliuchnikova@sam-solutions.com", Country = country3 };
            Client client4 = new Client { FirstName = "Наталия", LastName = "Ключникова", Login = "natalia", Password = "123", Email = "n.kliuchnikova@sam-solutions.com", Country = country4 };

            db.Clients.Add(client1);
            db.Clients.Add(client2);
            db.Clients.Add(client3);

            //Hotels
            Hotel hotel1 = new Hotel { Name = "Парус", Country = country1, Stars = 3, IsAppartment = false };
            Hotel hotel2 = new Hotel { Name = "Олимп", Country = country2, Stars = 3, IsAppartment = false };
            Hotel hotel3 = new Hotel { Name = "Вивульскио", Country = country3, Stars = 3, IsAppartment = false };
            Hotel hotel4 = new Hotel { Name = "Шерлок", Country = country4, Stars = 3, IsAppartment = true };

            db.Hotels.Add(hotel1);
            db.Hotels.Add(hotel2);
            db.Hotels.Add(hotel3);
            db.Hotels.Add(hotel4);
            
            db.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult CreateRoom()
        {
            IEnumerable<Hotel> hotels = _hotelManager.GetAll();

            RoomViewModel rhvm = new RoomViewModel { Hotels = hotels };

            return View(rhvm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(Room room)
        {
            await _roomManager.AddAsync(room);

            return RedirectToAction("CreateRoom");
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateRoom(string HotelId, string Description, int MaxNumberOfGuests, int Price, bool IsBalcony, bool IsKitchen)
        //{
        //    return RedirectToAction("CreateRoom");
        //}
    }
}
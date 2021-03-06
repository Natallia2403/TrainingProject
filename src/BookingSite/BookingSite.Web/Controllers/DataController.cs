﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BookingSite.Web.Controllers
{
    public class DataController : Controller
    {
        DataContext _dataContext;
        IHotelRepository _hotelRepository;
        ICountryRepository _countryRepository;
        IRoomRepository _roomRepository;
        UserManager<User> _userRepository;
        RoleManager<IdentityRole> _roleManager;

        public DataController(DataContext context, IHotelRepository hotelRepositary, ICountryRepository countryRepositary, IRoomRepository roomRepositary
                                , UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            _hotelRepository = hotelRepositary ?? throw new ArgumentNullException(nameof(hotelRepositary));
            _countryRepository = countryRepositary ?? throw new ArgumentNullException(nameof(countryRepositary));
            _roomRepository = roomRepositary ?? throw new ArgumentNullException(nameof(roomRepositary));

            _userRepository = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));

            _dataContext = context;
        }

        public async Task<IActionResult> AddAsync()
        {
            //Users & Roles
            string adminEmail = "natallia.2403@gmail.com";
            string password = "Aa1234567+";

            if (await _roleManager.FindByNameAsync("admin") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await _roleManager.FindByNameAsync("manager") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole("manager"));
            }
            if (await _userRepository.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail, FirstName = "Наталия", LastName = "Ключникова" };
                IdentityResult result = await _userRepository.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await _userRepository.AddToRoleAsync(admin, "admin");
                    await _userRepository.AddToRoleAsync(admin, "manager");
                }
            }

            //Countries
            Country country1 = new Country { Name = "Беларусь" };
            Country country2 = new Country { Name = "Россия" };
            Country country3 = new Country { Name = "Украина" };
            Country country4 = new Country { Name = "Литва" };
            Country country5 = new Country { Name = "Швеция" };
            Country country6 = new Country { Name = "Финляндия" };

            _dataContext.Countries.Add(country1);
            _dataContext.Countries.Add(country2);
            _dataContext.Countries.Add(country3);
            _dataContext.Countries.Add(country4);
            _dataContext.Countries.Add(country5);
            _dataContext.Countries.Add(country6);

            //Hotels
            var userId = _userRepository.FindByNameAsync(adminEmail).Result.Id;

            Hotel hotel1 = new Hotel { UserId = userId, Name = "Парус", Country = country1, Stars = 3, IsAppartment = false };
            Hotel hotel2 = new Hotel { UserId = userId, Name = "Олимп", Country = country2, Stars = 3, IsAppartment = false };
            Hotel hotel3 = new Hotel { UserId = userId, Name = "Вивульскио", Country = country3, Stars = 3, IsAppartment = false };
            Hotel hotel4 = new Hotel { UserId = userId, Name = "Шерлок", Country = country4, Stars = 3, IsAppartment = true };

            _dataContext.Hotels.Add(hotel1);
            _dataContext.Hotels.Add(hotel2);
            _dataContext.Hotels.Add(hotel3);
            _dataContext.Hotels.Add(hotel4);

            //Rooms
            Room room1 = new Room { Hotel = hotel1, Description="Номер Люкс", HasBalcony = true, HasKitchen = true, MaxNumberOfGuests=4, Price=100};
            Room room2 = new Room { Hotel = hotel1, Description = "Номер Эконом", HasBalcony = false, HasKitchen = false, MaxNumberOfGuests=2, Price=20};

            _dataContext.Rooms.Add(room1);
            _dataContext.Rooms.Add(room2);

            _dataContext.SaveChanges();

            return View();
        }
    }
}
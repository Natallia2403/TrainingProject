using BookingSite.Domain.Logic.Interfaces;
using BookingSite.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using BookingSite.Domain.DTO;
using BookingSite.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq.Expressions;
using System.Threading;
using System.Collections;

namespace BookingSite.Tests
{
    public class ManageControllerTests
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfUsers()
        {
            IEnumerable<HotelDTO> hotelDTOs = GetTestHotels();

            //Arrange
            var mock = new Mock<IHotelManager>();
            mock.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult((hotelDTOs)));
            var controller = new ManageController(null, mock.Object, null, null, null, null);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
        }

        private void GetTestData()
        {
            //Countries
            List<Country> countries = new List<Country>();

            Country country1 = new Country { Name = "Беларусь" };
            Country country2 = new Country { Name = "Россия" };
            Country country3 = new Country { Name = "Украина" };
            Country country4 = new Country { Name = "Литва" };
            Country country5 = new Country { Name = "Швеция" };
            Country country6 = new Country { Name = "Финляндия" };

            countries.Add(country1);
            countries.Add(country2);
            countries.Add(country3);
            countries.Add(country4);
            countries.Add(country5);
            countries.Add(country6);



            ////Rooms
            //List<Room> rooms = new List<Room>();
            //Room room1 = new Room { Hotel = hotel1, Description = "Номер Люкс", HasBalcony = true, HasKitchen = true, MaxNumberOfGuests = 4, Price = 100 };
            //Room room2 = new Room { Hotel = hotel1, Description = "Номер Эконом", HasBalcony = false, HasKitchen = false, MaxNumberOfGuests = 2, Price = 20 };

            //_dataContext.Rooms.Add(room1);
            //_dataContext.Rooms.Add(room2);
        }

        private List<HotelDTO> GetTestHotels()
        {
            //Countries
            List<CountryDTO> countries = new List<CountryDTO>();

            CountryDTO country1 = new CountryDTO { Name = "Беларусь" };
            CountryDTO country2 = new CountryDTO { Name = "Россия" };

            countries.Add(country1);
            countries.Add(country2);

            //Hotels
            List<HotelDTO> hotels = new List<HotelDTO>();
            HotelDTO hotel1 = new HotelDTO { Name = "Парус", Country = country1, Stars = 3, IsAppartment = false };
            HotelDTO hotel2 = new HotelDTO { Name = "Олимп", Country = country2, Stars = 3, IsAppartment = false };
            HotelDTO hotel3 = new HotelDTO { Name = "Вивульскио", Country = country1, Stars = 3, IsAppartment = false };
            HotelDTO hotel4 = new HotelDTO { Name = "Шерлок", Country = country2, Stars = 3, IsAppartment = true };

            hotels.Add(hotel1);
            hotels.Add(hotel2);
            hotels.Add(hotel3);
            hotels.Add(hotel4);

            return hotels;
        }

    }
}

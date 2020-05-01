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
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace BookingSite.Tests
{
    public class ManageControllerTests
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfUsers()
        {
            IEnumerable<HotelDTO> hotelDTOs = GetTestHotels();
            var user = GetTestUser();

            //Arrange
            var mockHotel = new Mock<IHotelManager>();
            var mockLogging = new Mock<ILogger<HomeController>>();
            var mockCountry = new Mock<ICountryManager>();
            var mockRoom = new Mock<IRoomManager>();
            var mockBooking = new Mock<IBookingManager>();
            var mockUser = new Mock<IUserManager>();

            //mockHotel.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult((hotelDTOs)));

            mockHotel.Setup(repo => repo.GetByUserIdAsync("")).Returns(Task.FromResult((hotelDTOs)));

            mockUser.Setup(repo => repo.GetCurrentUserId(null)).Returns(("natallia.2403@gmail.com"));

            // mockUser.Setup(repo => repo.FindByNameAsync("")).Returns((user));

            var controller = new ManageController(mockLogging.Object, mockHotel.Object, mockCountry.Object, mockRoom.Object, mockUser.Object, mockBooking.Object);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
        }

        private User GetTestUser()
        {
            string adminEmail = "natallia.2403@gmail.com";

            User admin = new User { Email = adminEmail, UserName = adminEmail, FirstName = "Наталия", LastName = "Ключникова" };

            return admin;
        }

        private IEnumerable<HotelDTO> GetTestHotels()
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

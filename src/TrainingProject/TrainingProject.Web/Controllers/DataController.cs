using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingProject.Data;
using TrainingProject.Data.Models;

namespace TrainingProject.Web.Controllers
{
    public class DataController : Controller
    {
        DataContext db;

        public DataController(DataContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

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

            //Cities
            City city1 = new City { Name = "Минск", Country = country1};
            City city2 = new City { Name = "Москва", Country = country2};
            City city3 = new City { Name = "Киев", Country = country3};
            City city4 = new City { Name = "Вильнюс", Country = country4};
            City city5 = new City { Name = "Стокгольм", Country = country5};
            City city6 = new City { Name = "Хельсинки", Country = country6};

            db.Cities.Add(city1);
            db.Cities.Add(city2);
            db.Cities.Add(city3);
            db.Cities.Add(city4);
            db.Cities.Add(city5);
            db.Cities.Add(city6);

            //Clients
            Client client1 = new Client { FirstName = "Иван", LastName = "Иванов", Login = "ivan", Password = "123", Email = "n.kliuchnikova@sam-solutions.com", Country = country1 };
            Client client2 = new Client { FirstName = "Пётр", LastName = "Петров", Login = "petr", Password = "123", Email = "n.kliuchnikova@sam-solutions.com", Country = country2 };
            Client client3 = new Client { FirstName = "Сидор", LastName = "Сидоров", Login = "sidor", Password = "123", Email = "n.kliuchnikova@sam-solutions.com", Country = country3 };
            Client client4 = new Client { FirstName = "Наталия", LastName = "Ключникова", Login = "natalia", Password = "123", Email = "n.kliuchnikova@sam-solutions.com", Country = country4 };

            db.Clients.Add(client1);
            db.Clients.Add(client2);
            db.Clients.Add(client3);

            //Hotels



            db.SaveChanges();

            return View();
        }
    }
}
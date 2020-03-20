using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainingProject.Data;
using TrainingProject.Data.Models;

namespace TrainingProject.Web.Controllers
{
    public class HomeController : Controller
    {
        DataContext db;
        private readonly ILogger _logger;

        public HomeController(DataContext context,
                                ILogger<HomeController> logger)
        {
            _logger = logger;

            context = context ?? throw new ArgumentNullException(nameof(context));

            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Countries.ToList());
        }
    }
}
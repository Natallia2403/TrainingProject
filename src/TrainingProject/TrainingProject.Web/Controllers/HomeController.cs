using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingProject.Data;
using TrainingProject.Data.Models;

namespace TrainingProject.Web.Controllers
{
    public class HomeController : Controller
    {
        DataContext db;
        public HomeController(DataContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Countries.ToList());
        }
    }
}
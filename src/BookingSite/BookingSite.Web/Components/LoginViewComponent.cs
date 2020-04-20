using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSite.Web.Components
{
    public class Login : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

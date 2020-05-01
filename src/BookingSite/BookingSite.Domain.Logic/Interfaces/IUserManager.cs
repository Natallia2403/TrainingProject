using BookingSite.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingSite.Domain.Logic.Interfaces
{
    public interface IUserManager
    {
        User FindByNameAsync(string userName);

        string GetCurrentUserName(HttpContext HttpContext);

        string GetCurrentUserId(HttpContext HttpContext);
    }
}

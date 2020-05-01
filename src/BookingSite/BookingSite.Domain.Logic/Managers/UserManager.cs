using AutoMapper;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingSite.Domain.Logic.Managers
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<User> _userManager;
        IMapper _mapper;

        public UserManager()
        {

        }

        public UserManager(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public User FindByNameAsync(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            return user;
        }

        public string GetCurrentUserName(HttpContext HttpContext)
        {
            if (HttpContext != null)
                return HttpContext.User.Identity.Name;
            else
                return string.Empty;
        }

        public string GetCurrentUserId(HttpContext HttpContext)
        {
            var name = GetCurrentUserName(HttpContext);

            var user = FindByNameAsync(name);

            return user.Id;
        }
    }
}

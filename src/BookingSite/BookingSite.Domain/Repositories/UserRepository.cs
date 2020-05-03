using AutoMapper;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingSite.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userRepository;
        IMapper _mapper;

        public UserRepository()
        {

        }

        public UserRepository(UserManager<User> userManager, IMapper mapper)
        {
            _userRepository = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public User FindByNameAsync(string userName)
        {
            var user = _userRepository.FindByNameAsync(userName).Result;

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

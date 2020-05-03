using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookingSite.Data.Models;
using BookingSite.Web.ViewModels;
using System.Diagnostics;

namespace BookingSite.Web.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// сервис по управлению пользователями 
        /// </summary>
        private readonly UserManager<User> _userRepository;
        /// <summary>
        /// сервис, который позволяет аутентифицировать пользователя и устанавливать или удалять его куки.
        /// </summary>
        private readonly SignInManager<User> _signInRepository;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userRepository = userManager;
            _signInRepository = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year.Value, FirstName = model.FirstName, LastName = model.LastName };
                // добавляем пользователя
                var result = await _userRepository.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // установка аутентификационных куки
                    await _signInRepository.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInRepository.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            string errorActionPath = $"~/Views/Error/HandleError.cshtml";

            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier,
                Message = "Доступ запрещен. Пожалуйста, обратитесь к Администратору.",
                StatusCode = (403, ""),
            };

            return View(errorActionPath, model);
        }
    }
}
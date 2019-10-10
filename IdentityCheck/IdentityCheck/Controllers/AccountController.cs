﻿using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels.Account;
using IdentityCheck.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityCheck.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IDateTimeService dateTimeService;

        public AccountController(IUserService userService, IDateTimeService dateTimeService)
        {
            this.userService = userService;
            this.dateTimeService = dateTimeService;
        }

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View(new RegisterRequest());
        }

        [HttpGet("/login")]
        public async Task<IActionResult> Login()
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View(new LoginRequest());
        }

        [HttpPost("/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var errors = await userService.LoginAsync(model);
                if (errors.Count == 0)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            return View(model);
        }


        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                var errors = await userService.RegisterAsync(model);
                if (errors.Count == 0)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                model.ErrorMessages = errors;
            }
            return View(model);
        }

        [Authorize]
        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await userService.Logout();
            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        [HttpGet("/settings")]
        public async Task<IActionResult> Settings()
        {
            var user = await userService.GetCurrentUserAsync();
            return View(nameof(Settings), user.TimeZoneId);
        }


        [HttpPost("/settings")]
        public async Task<IActionResult> Settings(string timeZoneId)
        {
            var user = await userService.GetCurrentUserAsync();
            await dateTimeService.SetUserTimeZoneAsync(user, timeZoneId);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [HttpPost("/setLanguage")]
        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //Google Auth Section

        [HttpGet("/Google-login")]
        public IActionResult GoogleLogin()
        {
            // We can set where we want to handle google's response
            var redirectUrl = "Google-response";

            // We use Identity to create our properties for the authentication the provider now is google
            var properties = userService.ConfigureExternalAuthenticationProperties("Google", redirectUrl);

            // Challenge expression here means that we challenge the external authentication provider
            // To handle the authentication. Something like "I don't know who this user is so I'm letting you
            // handle it" 
            return new ChallengeResult("Google", properties);
        }

        [HttpGet("/Google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            // Identity handles the response and stores it to an "ExternalLoginInfo" object
            var info = await userService.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // The framework let's you sign in with the login provider which is google in this case and
            // the provider key. Which is uniqueId for the IdentityUser generated by the loginprovider
            var result = await userService.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (!result.Succeeded)
            {
                await userService.CreateAndLoginGoogleUser(info);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}

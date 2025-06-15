using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using WeatherForecast.Data;
using WeatherForecast.Service;

namespace WeatherForecast.Controllers
{
    /*
     * AUTH CONTROLLER MAIN CLASS
     */
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        /*
         * SHOW LOGIN FORM
         */
        public IActionResult Index()
        {
            return View();
        }

        /*
         * USER LOGIN
         */
        [HttpPost]
        public IActionResult Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                var _user = _authService.CheckCredential(user);
                if (_user == null)
                {
                    TempData["error"] = "Credentials do not match.";
                    return RedirectToAction("Index", "Auth");
                }
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Role, _user.UserRole));
                identity.AddClaim(new Claim(ClaimTypes.Email, _user.UserEmail));
                HttpContext.Session.SetString("UserEmail", _user.UserEmail);
                HttpContext.Session.SetInt32("UserId", _user.UserId);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                if (_user.UserRole == "Admin")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    return RedirectToAction("Index", "Weather");
                }
                else if (_user.UserRole == "User")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                    return RedirectToAction("Index", "Weather");
                }

            }
            TempData["error"] = "Credentials do not match.";
            return RedirectToAction("Index", "Auth");
        }
        /*
         * LOGOUT
         */
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Session.Clear();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while logging out.";
                return RedirectToAction("Index");
            }
        }

        /*
         * SIGN UP
         */
        public IActionResult Signup()
        {
            return View();
        }

        /*
         * SIGN UP
         */
        [HttpPost]
        public async Task<IActionResult> Signup(User user)
        {
            var status = await _authService.Signup(user);
            if (status)
            {
                TempData["success"] = "Account created successfully!";
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                TempData["error"] = "An error occurred while creating user.";
                return RedirectToAction("Signup", "Auth");
            }
            //TempData["error"] = "An error occurred while creating user.";
            //return View();
        }

        public IActionResult Forbidden()
        {
            TempData["error"] = "Permissin Denied!";
            return RedirectToAction("Index", "Auth");
        }

    }
}
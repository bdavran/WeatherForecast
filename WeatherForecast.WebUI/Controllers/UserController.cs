﻿using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Data;
using WeatherForecast.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Identity;

namespace WeatherForecast.Controllers
{
    [Authorize(Policy = "Admin")]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(ForecastContext _context, IConfiguration _config, IPasswordHasher<User> passwordHasher)
        {
            _userService = new UserService(_context, _config, passwordHasher);
        }

        /*
         * GET LIST OF USERS
         */
        public async Task<IActionResult> Index(string str, int page = 1)
        {
            var users = _userService.GetAll(str);
            int pageSize = 7;
            var getList = await PaginatedList<User>.CreateSyncList(users, page, pageSize);
            return View(getList);
        }

        /*
         * NEW USER CREATE FORM
         */
        public IActionResult Create()
        {
            return View();
        }

        /*
         * USER EDIT FORM
         */
        
        public IActionResult Edit(int[] userId)
        {
            if (userId.Length == 0)
            {
                TempData["error"] = "Please select user to edit!";
                return RedirectToAction("Index");
            }
            if (userId.Length != 1)
            {
                TempData["error"] = "Please select only one user to edit!";
                return RedirectToAction("Index");
            }
            var user = _userService.GetUser(userId[0]);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        /*
         * EDIT USER
         */
        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel userModel)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var status = await _userService.UpdateUser(userModel, email);
            if (status)
            {
                TempData["success"] = "User edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Error occurred while editing user";
                return RedirectToAction("Index");
            }
        }

        /*
         * CREATE NEW USER
         */
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create(User user)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var status =  await _userService.Create(user, email);
            if (status)
            {
                TempData["success"] = "User created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Error occurred while creating user";
                return View();
            }
        }

        /*
         * DELETE USER BY ID
         */
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int[] userId)
        {
            if (userId.Length == 0)
            {
                TempData["error"] = "Please select user to delete!";
                return RedirectToAction("Index");
            }
            var email = HttpContext.Session.GetString("UserEmail");
            var status = await _userService.Delete(userId, email);
            if (status)
            {
                TempData["success"] = "User deleted successfully";
            }
            else
            {
                TempData["error"] = "Error occurred while deleting user";
            }
            return RedirectToAction("Index");
        }

    }
}

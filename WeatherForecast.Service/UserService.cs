using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WeatherForecast.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WeatherForecast.Service
{
    public class UserService
    {
        private readonly ForecastContext _context;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(ForecastContext context, IConfiguration config, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _config = config;
            _passwordHasher = passwordHasher;
        }

        /*
         * GET LIST OF USERS
         */
        [HttpGet]
        public List<User> GetAll(string str)
        {
            var _users = _context.Users.ToList();
            if (!string.IsNullOrEmpty(str))
            {
                var searchedItems = _users.Where(x => x.City.Contains(str, StringComparison.OrdinalIgnoreCase) || x.UserEmail.Contains(str, StringComparison.OrdinalIgnoreCase) || x.UserRole.Contains(str, StringComparison.OrdinalIgnoreCase)).ToList();
                return searchedItems;
            }
            return _users;
        }
        [HttpGet]
        public UserViewModel GetUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            var userViewModel = new UserViewModel
            {
                UserEmail = user.UserEmail,
                UserId = user.UserId,
                City = user.City,
                Cities = TurkeyCities._turkeyCities.Select(city => new SelectListItem
                {
                    Value = city,
                    Text = city
                }).ToList(),
                UserRole = user.UserRole,
                Roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "User", Text = "User" },
                new SelectListItem { Value = "Admin", Text = "Admin" }
            }
            };
            if (userViewModel == null)
            {
                return null;
            }
            return userViewModel;
        }

        /*
         * CREATE USER
         */
        [HttpPost]
        public async Task<bool> UpdateUser(UserViewModel user, string userEmail)
        {
            if (string.IsNullOrWhiteSpace(user.City) || string.IsNullOrWhiteSpace(user.UserEmail) || string.IsNullOrWhiteSpace(user.password)
                || string.IsNullOrWhiteSpace(user.UserRole) || string.IsNullOrWhiteSpace(user.UserId.ToString()))
            {
                return false;
            }
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            if (existingUser == null)
            {
                return false; 
            }
            try
            {
                existingUser.City = user.City;
                existingUser.UserEmail = user.UserEmail;
                existingUser.UserRole = user.UserRole;
                existingUser.password = _passwordHasher.HashPassword(existingUser, user.password);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var exp = ex;
                return false;
            }
        }

        /*
         * CREATE USER
         */
        [HttpPost]
        public async Task<bool> Create(User user, string userEmail)
        {
            if (string.IsNullOrWhiteSpace(user.City) || string.IsNullOrWhiteSpace(user.UserEmail) || string.IsNullOrWhiteSpace(user.password)
                || string.IsNullOrWhiteSpace(user.UserRole))
            {
                return false;
            }
            User item = new User()
            {
                City = user.City,
                UserEmail = user.UserEmail,
                password = user.password,
                UserRole = user.UserRole,
            };
            item.password = _passwordHasher.HashPassword(item, item.password);
            try
            {
                _context.Users.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var exp = ex;
                return false;
            }
        }

        /*
         * DELETE USER
         */
        [HttpPost]
        public async Task<bool> Delete(int[] userIds, string userEmail)
        {
            if (userIds.Length == 0)
            {
                return false;
            }
            try
            {
                var _user = _context.Users.Where(o => userIds.Contains(o.UserId)).ToList();
                _context.Users.RemoveRange(_user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var exp = ex;
                return false;
            }

        }

    }
}

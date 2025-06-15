using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WeatherForecast.Data;

namespace WeatherForecast.Service
{
    public class AuthService
    {
        private readonly ForecastContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AuthService(ForecastContext db, IPasswordHasher<User> passwordHasher)
        {
            _context = db;
            _passwordHasher = passwordHasher;
        }

        /*
         * USER LOGIN CREDENTIAL CHECK
         */
        public User CheckCredential(UserLogin user)
        {
            var _user = _context.Users.FirstOrDefault(u => u.UserEmail == user.UserEmail);
            if (_user != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(_user, _user.password, user.password);
                if (result == PasswordVerificationResult.Success)
                {
                    return _user;
                }
            }
            return null;
        }


        /*
         * SIGN UP
         */
        public async Task<bool> Signup(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserEmail) || string.IsNullOrWhiteSpace(user.password))
            {
                return false;
            }
            User item = new User()
            {
                City = user.City,
                UserEmail = user.UserEmail,
                password = user.password,
                UserRole = "User",
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
    }
}

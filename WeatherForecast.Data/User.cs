using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Data
{
    public class User
    {
        public User()
        {
        }
        public static ClaimsIdentity Identity { get; set; }
        [Key]
        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string UserRole { get; set; }
    }
}

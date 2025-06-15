using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Data
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string City { get; set; }
        public string password { get; set; }
        public string UserRole { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> Cities { get; set; }
    }
}

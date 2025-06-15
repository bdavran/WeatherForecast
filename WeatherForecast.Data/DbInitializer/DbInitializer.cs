using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Data;

namespace WeatherForecast.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ForecastContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public DbInitializer(ForecastContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public void Initialize()
        {

            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            /*if (!_context.Users.Any(u => u.UserRole == "Admin"))
            {
                var adminUser = new User
                {
                    UserEmail = "admin@gmail.com",
                    City = "Istanbul",
                    password = "Admin",
                    UserRole = "Admin"
                };

                adminUser.password = _passwordHasher.HashPassword(adminUser, adminUser.password);

                _context.Users.Add(adminUser);
                _context.SaveChanges();
            }*/

            /*if (!_context.Cities.Any())
            {
                var turkiyeIlleri = new List<City>
            {
                new City { CityName = "Adana" },
                new City { CityName = "Adıyaman" },
                new City { CityName = "Afyonkarahisar" },
                new City { CityName = "Ağrı" },
                new City { CityName = "Amasya" },
                new City { CityName = "Ankara" },
                new City { CityName = "Antalya" },
                new City { CityName = "Artvin" },
                new City { CityName = "Aydın" },
                new City { CityName = "Balıkesir" },
                new City { CityName = "Bilecik" },
                new City { CityName = "Bingöl" },
                new City { CityName = "Bitlis" },
                new City { CityName = "Bolu" },
                new City { CityName = "Burdur" },
                new City { CityName = "Bursa" },
                new City { CityName = "Çanakkale" },
                new City { CityName = "Çankırı" },
                new City { CityName = "Çorum" },
                new City { CityName = "Denizli" },
                new City { CityName = "Diyarbakır" },
                new City { CityName = "Edirne" },
                new City { CityName = "Elazığ" },
                new City { CityName = "Erzincan" },
                new City { CityName = "Erzurum" },
                new City { CityName = "Eskişehir" },
                new City { CityName = "Gaziantep" },
                new City { CityName = "Giresun" },
                new City { CityName = "Gümüşhane" },
                new City { CityName = "Hakkari" },
                new City { CityName = "Hatay" },
                new City { CityName = "Isparta" },
                new City { CityName = "Mersin" },
                new City { CityName = "İstanbul" },
                new City { CityName = "İzmir" },
                new City { CityName = "Kars" },
                new City { CityName = "Kastamonu" },
                new City { CityName = "Kayseri" },
                new City { CityName = "Kırklareli" },
                new City { CityName = "Kırşehir" },
                new City { CityName = "Kocaeli" },
                new City { CityName = "Konya" },
                new City { CityName = "Kütahya" },
                new City { CityName = "Malatya" },
                new City { CityName = "Manisa" },
                new City { CityName = "Kahramanmaraş" },
                new City { CityName = "Mardin" },
                new City { CityName = "Muğla" },
                new City { CityName = "Muş" },
                new City { CityName = "Nevşehir" },
                new City { CityName = "Niğde" },
                new City { CityName = "Ordu" },
                new City { CityName = "Rize" },
                new City { CityName = "Sakarya" },
                new City { CityName = "Samsun" },
                new City { CityName = "Siirt" },
                new City { CityName = "Sinop" },
                new City { CityName = "Sivas" },
                new City { CityName = "Tekirdağ" },
                new City { CityName = "Tokat" },
                new City { CityName = "Trabzon" },
                new City { CityName = "Tunceli" },
                new City { CityName = "Şanlıurfa" },
                new City { CityName = "Uşak" },
                new City { CityName = "Van" },
                new City { CityName = "Yozgat" },
                new City { CityName = "Zonguldak" },
                new City { CityName = "Aksaray" },
                new City { CityName = "Bayburt" },
                new City { CityName = "Karaman" },
                new City { CityName = "Kırıkkale" },
                new City { CityName = "Batman" },
                new City { CityName = "Şırnak" },
                new City { CityName = "Bartın" },
                new City { CityName = "Ardahan" },
                new City { CityName = "Iğdır" },
                new City { CityName = "Yalova" },
                new City { CityName = "Karabük" },
                new City { CityName = "Kilis" },
                new City { CityName = "Osmaniye" },
                new City { CityName = "Düzce" }
            };

                _context.Cities.AddRange(turkiyeIlleri);
                _context.SaveChanges();
            }*/

        }
    }
}

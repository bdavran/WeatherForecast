using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WeatherForecast.Data
{
    public class ForecastContext: IdentityDbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public ForecastContext(DbContextOptions<ForecastContext> options) : base(options)
        {
        }

       /* public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }*/

    }
}

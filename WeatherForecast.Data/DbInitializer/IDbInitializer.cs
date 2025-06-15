using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Data.DbInitializer
{
    public interface IDbInitializer
    {
        public void Initialize();
    }
}

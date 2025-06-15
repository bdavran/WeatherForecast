using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WeatherForecast.Data;

[Authorize]
public class WeatherController : Controller
{
    private readonly string _apiKey = "bda8b9b50d4685fe97cfd513f599c753";

    public async Task<IActionResult> Index(string? city)
    {
        if (string.IsNullOrEmpty(city))
        {
            var identity = (ClaimsIdentity)User.Identity;
            city = identity.FindFirst("City")?.Value;
        }

        WeatherViewModel weather = await GetWeatherDataAsync(city);

        ViewBag.Cities = TurkeyCities._turkeyCities.Select(c => new SelectListItem
        {
            Value = c,
            Text = c,
            Selected = c == city
        }).ToList();

        return View(weather);
    }

    private async Task<WeatherViewModel> GetWeatherDataAsync(string city)
    {
        var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";

        using var client = new HttpClient();
        var response = await client.GetStringAsync(url);

        dynamic json = JsonConvert.DeserializeObject(response);

        return new WeatherViewModel
        {
            City = json.name,
            Main = json.weather[0].main,
            Description = json.weather[0].description,
            Temperature = json.main.temp,
            FeelsLike = json.main.feels_like,
            Pressure = json.main.pressure,
            Humidity = json.main.humidity,
            WindSpeed = json.wind.speed,
            Latitude = json.coord.lat,
            Longitude = json.coord.lon
        };
    }
}
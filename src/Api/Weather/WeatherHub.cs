using System.Threading.Tasks;
using Dashboard.Api.Weather.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace Dashboard.Api.Weather
{
    public interface IWeatherHub
    {
        Task Update(string zipCode, WeatherDto dto);
    }

    public class WeatherHub : Hub, IWeatherHub
    {
        private readonly IHubContext<WeatherHub> _context;

        public WeatherHub(IHubContext<WeatherHub> context)
        {
            _context = context;
        }

        public async Task Update(string zipCode, WeatherDto dto)
        {
            await _context.Clients.Group(zipCode).InvokeAsync("update", dto);
        }

        public async Task<WeatherDto> Get(string zipCode)
        {
            await Groups.AddAsync(zipCode);
            return new WeatherDto();
        }
    }
}

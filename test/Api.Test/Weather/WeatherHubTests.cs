using System;
using System.Threading.Tasks;
using Dashboard.Api.General.Actions;
using Dashboard.Api.Timers.Actions;
using Dashboard.Api.Weather.Dtos;
using Dashboard.TestDoubles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Sockets;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dashboard.Api.Test.Weather
{
    [TestClass]
    public class WeatherHubTests
    {
        private TestServer _server;
        private IActionSource _actionSource;

        [TestInitialize]
        public void Initialize()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<FakeStartup<Startup>>());
            _actionSource = _server.Host.Services.GetService<IActionSource>();
        }

        [TestMethod]
        public async Task HubShouldReturnWeatherForZipCode()
        {
            var connection = await StartHubConnection();
            var weatherDto = await connection.Invoke<WeatherDto>("get", "50035");

            Assert.IsNotNull(weatherDto);
        }

        [TestMethod]
        public async Task HubShouldSendUpdatedWeatherForZipCode()
        {
            var connection = await StartHubConnection();
            await connection.Invoke<WeatherDto>("get", "50035");

            var tcs = new TaskCompletionSource<WeatherDto>();
            connection.On("update", new[] {typeof(WeatherDto)}, dto => tcs.SetResult(dto[0] as WeatherDto));

            await _actionSource.Dispatch(new TimerElapsedAction());
            var weatherDto = await tcs.Task;
            Assert.IsNotNull(weatherDto);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _server.Dispose();
        }

        private async Task<HubConnection> StartHubConnection()
        {
            var uri = new Uri(_server.BaseAddress, "/streams/weather");

            var connection = new HubConnection(uri);
            await connection.StartAsync(TransportType.LongPolling, _server.CreateClient());
            return connection;
        }
    }
}

using System.Net.Http;
using System.Threading.Tasks;
using Dashboard.Api.Monitoring.Dtos;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dashboard.Api.Test
{
    [TestClass]
    public class StartupTests
    {
        private TestServer _server;
        private HttpClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [TestMethod]
        public async Task StartupShouldSayHello()
        {
            var response = await _client.GetStringAsync("/hello");
            Assert.AreEqual("Hello World!", response);
        }

        [TestMethod]
        public async Task StartupShouldGetCpuUsage()
        {
            var json = await _client.GetStringAsync("/monitoring");
            var dto = JsonConvert.DeserializeObject<MonitoringDto>(json);
            Assert.AreEqual("35 %", dto.CpuUsage);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _server.Dispose();
        }
    }
}

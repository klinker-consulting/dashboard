using System.Threading.Tasks;
using Dashboard.Api.Monitoring.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Api.Monitoring
{
    [Route("monitoring")]
    public class MonitoringController : Controller
    {
        [HttpGet("")]
        public Task<IActionResult> Get()
        {
            return Task.FromResult<IActionResult>(Ok(new MonitoringDto {CpuUsage = "35 %"}));
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using Dashboard.Api.General.Actions;
using Dashboard.Api.Monitoring.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Api.Monitoring
{
    [Route("monitoring")]
    public class MonitoringController : Controller
    {
        private readonly IActionSource _actionSource;

        public MonitoringController(IActionSource actionSource)
        {
            _actionSource = actionSource;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var actions = await _actionSource.GetActions();
            return Ok(new MonitoringDto
            {
                EventCount = actions.LongCount()
            });
        }
    }
}

using System.Threading.Tasks;
using Dashboard.Api.General.Actions;
using Dashboard.Api.Monitoring;
using Dashboard.Api.Monitoring.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dashboard.Api.Test.Monitoring
{
    [TestClass]
    public class MonitoringControllerTests
    {
        private MonitoringController _controller;
        private ActionSource _actionSource;

        [TestInitialize]
        public void Initialize()
        {
            _actionSource = new ActionSource();
            _controller = new MonitoringController(_actionSource);
        }

        [TestMethod]
        public async Task GetShouldGetEventCount()
        {
            await _actionSource.Dispatch(new Action(""));
            await _actionSource.Dispatch(new Action(""));

            var result = (OkObjectResult)await _controller.Get();
            Assert.AreEqual(2, ((MonitoringDto) result.Value).EventCount);
        }
    }
}

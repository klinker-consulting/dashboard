using System.Threading.Tasks;
using Dashboard.Api.General.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dashboard.Api.Test.General.Actions
{
    [TestClass]
    public class ActionSourceTests
    {
        private ActionSource _actionSrouce;

        [TestInitialize]
        public void Initialize()
        {
            _actionSrouce = new ActionSource();
        }

        [TestMethod]
        public async Task DispachShouldSendActionToAllListeners()
        {
            var action = new Action("");
            Action receivedAction = null;
            _actionSrouce.Listen(a => receivedAction = a);

            await _actionSrouce.Dispatch(action);
            Assert.AreSame(action, receivedAction);
        }

        [TestMethod]
        public async Task DispatchShouldNotDispatchToStoppedListeners()
        {
            var action = new Action("");
            Action receivedAction = null;
            _actionSrouce.Listen(a => receivedAction = a).Dispose();

            await _actionSrouce.Dispatch(action);
            Assert.IsNull(receivedAction);
        }

        [TestMethod]
        public async Task DispatchShouldRecordActionsUsingInMemoryRecordingStrategy()
        {
            var action = new Action("");

            await _actionSrouce.Dispatch(action);
            var actions = await _actionSrouce.GetActions();
            Assert.AreEqual(1, actions.Length);
        }
    }
}

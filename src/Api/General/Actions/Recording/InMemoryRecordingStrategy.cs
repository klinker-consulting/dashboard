using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Dashboard.Api.General.Actions.Recording
{
    public class InMemoryRecordingStrategy : IActionRecordingStrategy
    {
        private readonly ConcurrentBag<Action> _actions;

        public InMemoryRecordingStrategy()
        {
            _actions = new ConcurrentBag<Action>();
        }

        public Task<ImmutableArray<Action>> GetAll()
        {
            return Task.FromResult(_actions.ToImmutableArray());
        }

        public Task Record(Action action)
        {
            _actions.Add(action);
            return Task.CompletedTask;
        }
    }
}

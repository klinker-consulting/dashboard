using System;
using System.Collections.Immutable;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Dashboard.Api.General.Actions.Recording;

namespace Dashboard.Api.General.Actions
{
    public interface IActionSource
    {
        Task Dispatch(Action action);
        Task<ImmutableArray<Action>> GetActions();
        IDisposable Listen(System.Action<Action> listener);
    }

    public class ActionSource : IActionSource
    {
        private readonly IActionRecordingStrategy _recordingStrategy;
        private readonly Subject<Action> _subject;

        public ActionSource(IActionRecordingStrategy recordingStrategy = null)
        {
            _recordingStrategy = recordingStrategy ?? new InMemoryRecordingStrategy();
            _subject = new Subject<Action>();
        }

        public async Task Dispatch(Action action)
        {
            _subject.OnNext(action);
            await _recordingStrategy.Record(action);
        }

        public Task<ImmutableArray<Action>> GetActions()
        {
            return _recordingStrategy.GetAll();
        }

        public IDisposable Listen(System.Action<Action> listener)
        {
            return _subject.Subscribe(listener);
        }
    }
}

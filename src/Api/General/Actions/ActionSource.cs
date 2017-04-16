using System;
using System.Collections.Immutable;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Dashboard.Api.General.Actions.Recording;

namespace Dashboard.Api.General.Actions
{
    public interface IActionSource
    {
        Task Dispatch<T>(T action) where T : Action;
        Task<ImmutableArray<Action>> GetActions();
        IDisposable Listen<T>(System.Action<T> listener) where T : Action;
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

        public async Task Dispatch<T>(T action) where T : Action
        {
            _subject.OnNext(action);
            await _recordingStrategy.Record(action);
        }

        public Task<ImmutableArray<Action>> GetActions()
        {
            return _recordingStrategy.GetAll();
        }

        public IDisposable Listen<T>(System.Action<T> listener) where T : Action
        {
            return _subject
                .OfType<T>()
                .Subscribe(listener);
        }
    }
}

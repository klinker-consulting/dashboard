using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Dashboard.Api.General.Actions.Recording
{
    public interface IActionRecordingStrategy
    {
        Task<ImmutableArray<Action>> GetAll();
        Task Record(Action action);
    }
}

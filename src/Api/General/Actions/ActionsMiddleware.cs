using System.Threading.Tasks;
using Dashboard.Api.General.Actions.Http;
using Microsoft.AspNetCore.Http;

namespace Dashboard.Api.General.Actions
{
    public class ActionsMiddleware
    {
        private readonly IActionSource _actionSource;
        private readonly RequestDelegate _next;

        public ActionsMiddleware(RequestDelegate next, IActionSource actionSource)
        {
            _next = next;
            _actionSource = actionSource;
        }

        public Task Invoke(HttpContext context)
        {
            _actionSource.Dispatch(new HttpRequestAction(context.Request));
            return this._next(context);
        }
    }
}

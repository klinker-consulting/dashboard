using Microsoft.AspNetCore.Http;

namespace Dashboard.Api.General.Actions.Http
{
    public class HttpRequestAction : Action<HttpRequest>
    {
        public HttpRequestAction(HttpRequest data) 
            : base("HttpRequest", data)
        {
        }
    }
}

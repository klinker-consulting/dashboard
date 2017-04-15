using Dashboard.Api.General.Actions;
using Microsoft.AspNetCore.Builder;

namespace Dashboard.Api
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseActions(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ActionsMiddleware>();
        }
    }
}
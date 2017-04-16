using Dashboard.Api.General.Actions;
using Dashboard.Api.Weather;
using Dashboard.Api.Weather.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dashboard.Api
{
    public class Startup
    {
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IActionSource, ActionSource>();
            services.AddTransient<IWeatherHub, WeatherHub>();
            services.AddSingleton<IWeatherUpdaterService, WeatherUpdaterService>();
            services.AddMvc();
            services.AddSignalR();
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMiddleware<ActionsMiddleware>()
                .UseMvc()
                .UseSignalR(r =>
                {
                    r.MapHub<WeatherHub>("/streams/weather");
                });
            app.UseWebSocketConnections();

            app.ApplicationServices.GetService<IWeatherUpdaterService>().Start();
        }
    }
}

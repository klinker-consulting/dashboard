using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dashboard.TestDoubles
{
    public class FakeStartup<T> where T : new()
    {
        private readonly T _innerStartup;
        private readonly MethodInfo _configureMethod;
        private readonly MethodInfo _configureServicesMethod;

        public FakeStartup()
        {
            _innerStartup = new T();
            _configureMethod = typeof(T).GetMethod("Configure");
            _configureServicesMethod = typeof(T).GetMethod("ConfigureServices");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddApplicationPart(typeof(T).GetTypeInfo().Assembly)
                .AddControllersAsServices();
            _configureServicesMethod.Invoke(_innerStartup, new object[]{services});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var parameters = _configureMethod.GetParameters();
            if (parameters.Length == 3)
                _configureMethod.Invoke(_innerStartup, new object[] {app, env, loggerFactory});
            else if (parameters.Length == 2)
                _configureMethod.Invoke(_innerStartup, new object[] {app, env});
            else
                _configureMethod.Invoke(_innerStartup, new object[] {app});
        }
    }
}

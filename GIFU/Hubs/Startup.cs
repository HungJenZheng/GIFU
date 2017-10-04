using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(GIFU.Startup))]

namespace GIFU
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.UseSqlServer(Variable.GetConnectionString);
            var config = new HubConfiguration { EnableDetailedErrors = true };
            app.MapSignalR(config);
        }
    }
}
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(GIFU.Startup))]

namespace GIFU
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(andyWqhFine.Startup))]
namespace andyWqhFine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

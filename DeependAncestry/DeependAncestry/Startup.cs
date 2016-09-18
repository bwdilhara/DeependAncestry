using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeependAncestry.Startup))]
namespace DeependAncestry
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Alstar.Startup))]
namespace Alstar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}

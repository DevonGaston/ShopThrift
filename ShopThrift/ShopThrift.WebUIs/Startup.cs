using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopThrift.WebUIs.Startup))]
namespace ShopThrift.WebUIs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

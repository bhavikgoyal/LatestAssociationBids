using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AssociationBids.Portal.Startup))]
namespace AssociationBids.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
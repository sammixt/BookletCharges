using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutomationOfWithdrawalBookletCharges.Startup))]
namespace AutomationOfWithdrawalBookletCharges
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

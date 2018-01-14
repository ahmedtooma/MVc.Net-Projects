using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobFindsDemo.Startup))]
namespace JobFindsDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

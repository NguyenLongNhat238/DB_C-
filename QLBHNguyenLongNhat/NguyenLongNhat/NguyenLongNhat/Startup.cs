using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NguyenLongNhat.Startup))]
namespace NguyenLongNhat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

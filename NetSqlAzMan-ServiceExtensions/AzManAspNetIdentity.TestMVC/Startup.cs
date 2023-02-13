using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AzManAspNetIdentity.TestMVC.Startup))]
namespace AzManAspNetIdentity.TestMVC {
	public partial class Startup {
		public void Configuration(IAppBuilder app) {
			ConfigureAuth(app);
		}
	}
}

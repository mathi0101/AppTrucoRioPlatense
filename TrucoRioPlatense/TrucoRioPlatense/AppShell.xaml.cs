using TrucoRioPlatense.Pages;

namespace TrucoRioPlatense {
	public partial class AppShell : Shell {
		public AppShell() {
			InitializeComponent();



			Routing.RegisterRoute(nameof(LoginViewPage), typeof(LoginViewPage));
			Routing.RegisterRoute(nameof(RegisterViewPage), typeof(RegisterViewPage));
			Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
		}
	}
}

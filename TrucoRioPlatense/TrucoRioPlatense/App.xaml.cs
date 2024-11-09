using TrucoRioPlatense.Pages;
using TrucoRioPlatense.ViewModels.Register;

namespace TrucoRioPlatense {
	public partial class App : Application {
		public App(IServiceProvider serviceProvider) {
			InitializeComponent();

			var loadinViewModel = serviceProvider.GetRequiredService<LoadingPageViewModel>();

			MainPage = new LoadingPage(loadinViewModel);
		}
	}
}

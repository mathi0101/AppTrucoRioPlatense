using TrucoRioPlatense.Pages;
using TrucoRioPlatense.Services.SecureStorageHandler;
using TrucoRioPlatense.ViewModels.Login;

namespace TrucoRioPlatense {
	public partial class AppShell : Shell {
		public AppShell() {
			InitializeComponent();


			Routing.RegisterRoute(nameof(MainMenuPage), typeof(MainMenuPage));

		}



		private async void SignOut_Clicked(object sender, EventArgs e) {
			if (await Current.DisplayAlert("Cerrar sesión?", "Estás seguro que quieres cerrar sesión?", "Ok", "Cancelar")) {
				await SSH.SetAsync(SSH.SSH_Keys_Enum.UserUid, "");
				await SSH.SetAsync(SSH.SSH_Keys_Enum.UserTokenId, "");
				await SSH.SetAsync(SSH.SSH_Keys_Enum.UserTokenExpireTime, "");
				await SSH.SetAsync(SSH.SSH_Keys_Enum.UserRefreshToken, "");

				var serviceProvider = (Application.Current as App)?.Handler.MauiContext.Services;
				var viewModel = serviceProvider.GetRequiredService<LoginViewPageModel>();
				Application.Current.MainPage = new NavigationPage(new LoginViewPage(viewModel));
			}
		}

	}
}

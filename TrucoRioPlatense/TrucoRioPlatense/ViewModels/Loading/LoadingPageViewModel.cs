using Firebase.Auth;
using TrucoRioPlatense.Features.Commands.Auth;
using TrucoRioPlatense.Models.LocalDatabase;
using TrucoRioPlatense.Models.Login;
using TrucoRioPlatense.Pages;
using TrucoRioPlatense.Services.SecureStorageHandler;
using TrucoRioPlatense.Services.Sqlite3;
using TrucoRioPlatense.ViewModels.Login;

namespace TrucoRioPlatense.ViewModels.Register {
	internal class LoadingPageViewModel : ViewModelBase {

		#region Propiedades

		#region Privadas
		private readonly FirebaseAuthClient _authClient;
		private readonly CurrentUserStore _currentUserStore;
		private readonly SQLiteDB _dbConnection;
		private readonly LoginCommand _loginCommand;
		private readonly LoginViewPageModel _loginViewModel;

		#endregion

		#region Publicas
		public string VersionNumber { get => "Version: " + AppInfo.VersionString; }
		#endregion

		#endregion

		#region Constructor

		public LoadingPageViewModel(FirebaseAuthClient authClient, CurrentUserStore currentUserStore, SQLiteDB dbConnection) {
			_authClient = authClient;
			_currentUserStore = currentUserStore;
			_dbConnection = dbConnection;

			_loginViewModel = new LoginViewPageModel(authClient, currentUserStore, dbConnection);

			_loginCommand = new LoginCommand(_loginViewModel, authClient, currentUserStore, dbConnection);
		}

		#endregion

		#region Eventos

		#endregion

		#region Metodos

		#region Privados



		#endregion

		#region Publicas

		internal async Task StartApplicaction() {
			await _dbConnection.PreloadDatabase();
			await Task.Delay(5000);

			// Hacemos prueba en Jira

			var tokenId = await _currentUserStore.GetUserTokenId(_authClient);
			if (string.IsNullOrEmpty(tokenId)) {
				Application.Current.MainPage = new NavigationPage(new LoginViewPage(_loginViewModel));
				return;
			} else {
				var sshUserUid = await SSH.GetAsync(SSH.SSH_Keys_Enum.UserUid);

				var userAccount = new UserAccounts();
				await userAccount.GetUserAsync(_dbConnection, u => u.Uid == sshUserUid);
			}
			Application.Current.MainPage = new NavigationPage(new MainPage());
			//await Shell.Current.GoToAsync(nameof(MainPage));
		}

		#endregion

		#endregion


	}
}

using Firebase.Auth;
using TrucoRioPlatense.Features.Commands.Auth;
using TrucoRioPlatense.Models.LocalDatabase;
using TrucoRioPlatense.Models.Login;
using TrucoRioPlatense.Pages;
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

		private async Task<UserAccounts> CheckUserInDatabaseAsync() {
			await _dbConnection.PreloadDatabase();



			var users = await _dbConnection.Connection.FindAsync<UserAccounts>(u => u.IsConnected);
			return users;
		}

		#endregion

		#region Publicas

		internal async Task LoadDataAsync() {
			await Task.Delay(2000);

			var user = await CheckUserInDatabaseAsync();

			if (user != null) {
				Application.Current.MainPage = new MainPage();
			} else {
				Application.Current.MainPage = new LoginViewPage(new LoginViewPageModel(_authClient, _currentUserStore, _dbConnection));
			}
		}

		#endregion

		#endregion


	}
}

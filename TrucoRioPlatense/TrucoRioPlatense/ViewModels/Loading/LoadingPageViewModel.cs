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

		private async Task<UserAccounts?> CheckUserInDatabaseAsync() {
			await _dbConnection.PreloadDatabase();

			var usr = new UserAccounts();

			return await usr.GetUserAsync(_dbConnection, u => !string.IsNullOrEmpty(u.Token)) ? usr : null;
		}

		#endregion

		#region Publicas

		internal async Task LoadDataAsync() {
			await Task.Delay(500);

			var user = await CheckUserInDatabaseAsync();

			//LoginViewPageModel viewModel = new LoginViewPageModel(_authClient, _currentUserStore, _dbConnection);
			if (user == null) {
				Application.Current.MainPage = new NavigationPage(new LoginViewPage(_loginViewModel));
				return;
			} else if (DateTime.Now > user.TokenExpireDate) {

				_loginViewModel.Email = user.Email;
				_loginViewModel.Password = user.Password;
				var result = await _loginCommand.ExecuteWithResultAsync(FirebaseProviderType.EmailAndPassword);

				;
				if (result.Value != Authentication_View_Response.Success) {
					Application.Current.MainPage = new NavigationPage(new LoginViewPage(_loginViewModel));
					return;
				}
			}

			Application.Current.MainPage = new NavigationPage(new MainPage());
			//await Shell.Current.GoToAsync(nameof(MainPage));
		}

		#endregion

		#endregion


	}
}

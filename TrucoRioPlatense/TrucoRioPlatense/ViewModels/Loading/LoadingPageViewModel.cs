using Firebase.Auth;
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

		#endregion

		#region Publicas

		#endregion

		#endregion

		#region Constructor

		public LoadingPageViewModel(FirebaseAuthClient authClient, CurrentUserStore currentUserStore, SQLiteDB dbConnection) {
			_authClient = authClient;
			_currentUserStore = currentUserStore;
			_dbConnection = dbConnection;
		}

		#endregion

		#region Eventos

		#endregion

		#region Metodos

		#region Privados

		private async Task<UserAccounts> CheckUserInDatabaseAsync() {
			await _dbConnection.PreloadDatabase();



			var users = await _dbConnection.Connection.FindAsync<UserAccounts>(u => u.IsConnected); // Implementa este método para obtener los usuarios
			return users; // Retorna el primer usuario si existe
		}

		#endregion

		#region Publicas

		internal async Task LoadDataAsync() {
			// Simula el tiempo de carga (puedes eliminar esto más tarde)
			await Task.Delay(2000);

			// Verifica si hay un usuario en la base de datos
			var user = await CheckUserInDatabaseAsync();

			if (user != null) {
				// Redirigir a la Main Page si hay un usuario
				Application.Current.MainPage = new MainPage();
			} else {
				// Redirigir al Login si no hay usuario
				Application.Current.MainPage = new LoginViewPage(new LoginViewPageModel(_authClient, _currentUserStore, _dbConnection));
			}
		}

		#endregion

		#endregion


	}
}

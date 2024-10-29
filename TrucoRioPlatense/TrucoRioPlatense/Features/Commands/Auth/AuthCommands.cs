using Firebase.Auth;
using TrucoRioPlatense.Models.LocalDatabase;
using TrucoRioPlatense.Models.Login;
using TrucoRioPlatense.Services.Sqlite3;
using TrucoRioPlatense.ViewModels.Login;
using TrucoRioPlatense.ViewModels.Register;

namespace TrucoRioPlatense.Features.Commands.Auth {
	internal class RegisterCommand : AsyncCommandBase {
		private readonly RegisterViewPageModel _viewModel;
		private readonly FirebaseAuthClient _authClient;
		private readonly CurrentUserStore _currentUserStore;
		private readonly SQLiteDB _dbConnection;

		public RegisterCommand(RegisterViewPageModel viewModel, FirebaseAuthClient authClient, CurrentUserStore currentUserStore, SQLiteDB dbConnection) {
			_viewModel = viewModel;
			_authClient = authClient;
			_currentUserStore = currentUserStore;
			_dbConnection = dbConnection;
		}


		protected override async Task<AsyncCommandBaseResult> ExecuteAsync(object? parameter) {
			try {
				UserCredential auth = await _authClient.CreateUserWithEmailAndPasswordAsync(_viewModel.Email, _viewModel.Password, _viewModel.DisplayName);
				if (auth != null) {
					_currentUserStore.AuthenticatedUserCredential = auth;
					await SaveNewLocalUser(auth);

					return AsyncCommandBaseResult.Success;
				} else {
					return AsyncCommandBaseResult.Unable;
				}
			} catch (FirebaseAuthHttpException ex) {
				return new AsyncCommandBaseResult(ex);
			}
		}

		private async Task SaveNewLocalUser(UserCredential auth) {
			UserAccounts user = new UserAccounts();
			user.CargarNuevoUsuario(auth);

			var actualUsers = await _dbConnection.Connection.Table<UserAccounts>().ToListAsync();
			if (actualUsers.Count > 0) {
				actualUsers.ForEach(x => {
					x.Token = null;
					x.TokenExpireDate = null;
				});
				await _dbConnection.Connection.UpdateAllAsync(actualUsers, true);
			}
			await user.InsertAsync(_dbConnection);
		}
	}

	internal class LoginCommand : AsyncCommandBase {
		private readonly LoginViewPageModel _loginviewModel;
		private readonly FirebaseAuthClient _authClient;
		private readonly CurrentUserStore _currentUserStore;
		private readonly SQLiteDB _dbConnection;


		public LoginCommand(LoginViewPageModel viewModel, FirebaseAuthClient authClient, CurrentUserStore currentUserStore, SQLiteDB dbConnection, Action<Exception>? onException = null) : base(onException) {
			_loginviewModel = viewModel;
			_authClient = authClient;
			_currentUserStore = currentUserStore;
			_dbConnection = dbConnection;
		}

		protected override async Task<AsyncCommandBaseResult> ExecuteAsync(object? parameter) {
			if (parameter is FirebaseProviderType providerType) {

				UserCredential? auth = null;
				switch (providerType) {
					case FirebaseProviderType.Unknown:
					auth = await _authClient.SignInWithCredentialAsync(_currentUserStore.AuthenticatedUserCredential.AuthCredential);
					break;
					case FirebaseProviderType.EmailAndPassword:
					auth = await _authClient.SignInWithEmailAndPasswordAsync(_loginviewModel.Email, _loginviewModel.Password);
					break;
					case FirebaseProviderType.Facebook:
					auth = await _authClient.SignInWithRedirectAsync(providerType, null);
					break;
					case FirebaseProviderType.Google:
					auth = await _authClient.SignInWithRedirectAsync(providerType, null);
					break;
					case FirebaseProviderType.Github:
					auth = await _authClient.SignInWithRedirectAsync(providerType, null);
					break;
					case FirebaseProviderType.Twitter:
					auth = await _authClient.SignInWithRedirectAsync(providerType, null);
					break;
					case FirebaseProviderType.Microsoft:
					auth = await _authClient.SignInWithRedirectAsync(providerType, null);
					break;
					case FirebaseProviderType.Apple:
					auth = await _authClient.SignInWithRedirectAsync(providerType, null);
					break;
					case FirebaseProviderType.Phone:
					auth = await _authClient.SignInWithRedirectAsync(providerType, null);
					break;
					case FirebaseProviderType.Anonymous:
					auth = await _authClient.SignInAnonymouslyAsync();
					break;
					default:
					break;
				}

				if (auth != null) {

					_currentUserStore.AuthenticatedUserCredential = auth;

					await AddLocalUser(auth, _dbConnection);

					return AsyncCommandBaseResult.Success;
				} else
					return AsyncCommandBaseResult.Unable;
			} else {
				return AsyncCommandBaseResult.Unable;
			}
		}
		private async Task AddLocalUser(UserCredential auth, SQLiteDB conn) {


			var actualUsers = await _dbConnection.Connection.Table<UserAccounts>().ToListAsync();
			actualUsers.ForEach(x => {
				x.Token = null;
				x.TokenExpireDate = null;
			});

			if (actualUsers.FindIndex(u => u.PlayerID == auth.User.Uid) is int ind && ind >= 0) {
				var usr = actualUsers[ind];
				usr.Token = auth.User.Credential.IdToken;
				usr.TokenExpireDate = auth.User.Credential.Created.AddSeconds(auth.User.Credential.ExpiresIn);
				usr.Password = _loginviewModel.Password;
			} else {
				UserAccounts user = new UserAccounts();
				user.CargarNuevoUsuario(auth);

				await user.InsertAsync(conn);
			}

			await _dbConnection.Connection.UpdateAllAsync(actualUsers, true);

		}
	}

}

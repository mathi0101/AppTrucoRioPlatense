using Firebase.Auth;
using TrucoRioPlatense.Helpers;
using TrucoRioPlatense.Models.LocalDatabase;
using TrucoRioPlatense.Models.Login;
using TrucoRioPlatense.Services.Sqlite3;
using TrucoRioPlatense.ViewModels.Login;
using TrucoRioPlatense.ViewModels.Register;

namespace TrucoRioPlatense.Features.Commands.Auth {
	internal class RegisterCommand : AsyncCommandBase<Authentication_View_Response> {
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


		protected override async Task<Authentication_View_Response> ExecuteAsync(object? parameter) {
			try {
				UserCredential auth = await _authClient.CreateUserWithEmailAndPasswordAsync(_viewModel.Email, _viewModel.Password, _viewModel.DisplayName);
				if (auth != null) {
					_currentUserStore.CurrentUser = auth.User;

					UserAccounts user = new UserAccounts() {
						PlayerID = auth.User.Uid,
						DisplayName = auth.User.Info.DisplayName,
						Email = auth.User.Info.DisplayName,
						IsConnected = true,
					};

					var result = await _dbConnection.Connection.InsertAsync(user);

					return Authentication_View_Response.Success;
				} else {
					return Authentication_View_Response.Unable;
				}
			} catch (FirebaseAuthHttpException ex) {
				return FirebaseHelper.ReturnUserAuthResponse(ex);
			}
		}
	}

	internal class LoginCommand : AsyncCommandBase<Authentication_View_Response> {
		private readonly LoginViewPageModel _viewModel;
		private readonly FirebaseAuthClient _authClient;
		private readonly CurrentUserStore _currentUserStore;
		private readonly SQLiteDB _dbConnection;

		public LoginCommand(LoginViewPageModel viewModel, FirebaseAuthClient authClient, CurrentUserStore currentUserStore, SQLiteDB dbConnection) {
			_viewModel = viewModel;
			_authClient = authClient;
			_currentUserStore = currentUserStore;
			_dbConnection = dbConnection;
		}


		protected override async Task<Authentication_View_Response> ExecuteAsync(object? parameter) {
			try {
				UserCredential auth = await _authClient.SignInWithEmailAndPasswordAsync(_viewModel.Email, _viewModel.Password);

				if (auth != null) {

					_currentUserStore.CurrentUser = auth.User;

					UserAccounts user = new UserAccounts() {
						PlayerID = auth.User.Uid,
						DisplayName = auth.User.Info.DisplayName,
						Email = auth.User.Info.DisplayName,
						IsConnected = true,
					};

					var result = await _dbConnection.Connection.InsertAsync(user);

					return result > 0 ? Authentication_View_Response.Success : Authentication_View_Response.Unable;
				} else
					return Authentication_View_Response.Unable;
			} catch (FirebaseAuthHttpException ex) {
				return FirebaseHelper.ReturnUserAuthResponse(ex);
			}
		}
	}

}

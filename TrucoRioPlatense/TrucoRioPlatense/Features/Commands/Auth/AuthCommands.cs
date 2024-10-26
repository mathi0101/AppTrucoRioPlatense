using Firebase.Auth;
using TrucoRioPlatense.Helpers;
using TrucoRioPlatense.Models.Login;
using TrucoRioPlatense.ViewModels.Login;
using TrucoRioPlatense.ViewModels.Register;

namespace TrucoRioPlatense.Features.Commands.Auth {
	internal class RegisterCommand : AsyncCommandBase<Authentication_View_Response> {
		private readonly RegisterViewPageModel _viewModel;
		private readonly FirebaseAuthClient _authClient;

		public RegisterCommand(RegisterViewPageModel viewModel, FirebaseAuthClient authClient) {
			_viewModel = viewModel;
			_authClient = authClient;
		}


		protected override async Task<Authentication_View_Response> ExecuteAsync(object? parameter) {
			try {
				var auth = await _authClient.CreateUserWithEmailAndPasswordAsync(_viewModel.Email, _viewModel.Password, _viewModel.DisplayName);
				return auth != null ? Authentication_View_Response.Success : Authentication_View_Response.Unable;
			} catch (FirebaseAuthHttpException ex) {
				return FirebaseHelper.ReturnUserAuthResponse(ex);
			}
		}
	}

	internal class LoginCommand : AsyncCommandBase<Authentication_View_Response> {
		private readonly LoginViewPageModel _viewModel;
		private readonly FirebaseAuthClient _authClient;

		public LoginCommand(LoginViewPageModel viewModel, FirebaseAuthClient authClient) {
			_viewModel = viewModel;
			_authClient = authClient;
		}


		protected override async Task<Authentication_View_Response> ExecuteAsync(object? parameter) {
			try {
				var auth = await _authClient.SignInWithEmailAndPasswordAsync(_viewModel.Email, _viewModel.Password);
				if (auth != null)
					return Authentication_View_Response.Success;
				else
					return Authentication_View_Response.Unable;
			} catch (FirebaseAuthHttpException ex) {
				return FirebaseHelper.ReturnUserAuthResponse(ex);
			}
		}
	}

}

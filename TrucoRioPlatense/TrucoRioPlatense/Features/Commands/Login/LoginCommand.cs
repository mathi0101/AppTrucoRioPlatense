using Firebase.Auth;
using TrucoRioPlatense.Helpers;
using TrucoRioPlatense.Models.Login;
using TrucoRioPlatense.ViewModels.Login;

namespace TrucoRioPlatense.Features.Commands.Login {
	internal class LoginCommand : AsyncCommandBase {
		private readonly LoginViewPageModel _viewModel;
		private readonly FirebaseAuthClient _authClient;

		public LoginCommand(LoginViewPageModel viewModel, FirebaseAuthClient authClient) {
			_viewModel = viewModel;
			_authClient = authClient;
		}


		protected override async Task<Authentication_View_Response> ExecuteAsync(object parameter) {
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

		internal override async Task InvokeAsync(object parameter) {
			await ExecuteAsync(parameter);
		}
	}
}

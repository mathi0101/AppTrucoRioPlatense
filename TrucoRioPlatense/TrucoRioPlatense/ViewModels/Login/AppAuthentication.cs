using Firebase.Auth;
using Newtonsoft.Json;
using TrucoRioPlatense.Services;
using static TrucoRioPlatense.Models.Login.LoginModels;

namespace TrucoRioPlatense.ViewModels.Login {
	internal class AppAuthentication(Guid guid) {

		public Guid Guid { get; } = guid;


		#region Privadas
		private FirebaseErrorResponse? GetJsonErrorResponse(FirebaseAuthHttpException ex) {
			var jsonString = ex.ResponseData;
			try {
				return JsonConvert.DeserializeObject<FirebaseErrorResponse>(jsonString);
			} catch (Exception) { return null; }

		}
		#endregion

		#region Publicas

		internal async Task<Authentication_View_Response> LogInAsync(string email, string password) {
			try {
				var auth = await ConexionFirebase.FirebaseAuthenticator.LogIn_UserAndPass(email, password);
				if (auth != null)
					return Authentication_View_Response.Success;
				else
					return Authentication_View_Response.Unable;
			} catch (FirebaseAuthHttpException ex) {
				switch (ex.Reason) {
					case AuthErrorReason.Unknown:
					if (GetJsonErrorResponse(ex) is FirebaseErrorResponse e)
						if (e.Error != null)
							if (e.Error.Message == "INVALID_LOGIN_CREDENTIALS")
								return Authentication_View_Response.AccountNotFound;
					return Authentication_View_Response.Unable;
					default:
					return Authentication_View_Response.Unable;
				}
			}
		}

		internal async Task<bool> RegisterUserAsync(string email, string password, string displayName) {
			try {
				var auth = await ConexionFirebase.FirebaseAuthenticator.Register_UserAndPass(email, password, displayName);
				return true;
			} catch (FirebaseAuthException ex) {
				return false;
			}
		}

		#endregion

		#region Enums

		internal enum Authentication_View_Response {
			Unable = -1,
			Success = 0,
			InvalidCredentials = 1,
			AccountLocked = 2,
			AccountNotFound = 3,
			TooManyAttempts = 4,
			PasswordExpired = 5,
		}

		#endregion
	}


}

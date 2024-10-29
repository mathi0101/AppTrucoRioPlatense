using Firebase.Auth;
using Newtonsoft.Json;
using TrucoRioPlatense.Models.Login;
using static TrucoRioPlatense.Models.Login.AuthLoginModels;

namespace TrucoRioPlatense.Helpers {
	internal static class FirebaseHelper {
		internal static Authentication_View_Response ReturnUserAuthResponse(FirebaseAuthHttpException ex) {
			switch (ex.Reason) {
				case AuthErrorReason.MissingEmail:
				return Authentication_View_Response.AccountNotFound;
				case AuthErrorReason.Unknown:
				if (FirebaseHelper.GetJsonErrorResponse(ex) is FirebaseErrorResponse e)
					if (e.Error != null)
						if (e.Error.Message == "INVALID_LOGIN_CREDENTIALS")
							return Authentication_View_Response.AccountNotFound;
				return Authentication_View_Response.Unable;
				default:
				return Authentication_View_Response.Unable;
			}
		}

		public static FirebaseErrorResponse? GetJsonErrorResponse(FirebaseAuthHttpException ex) {
			var jsonString = ex.ResponseData;
			try {
				return JsonConvert.DeserializeObject<FirebaseErrorResponse>(jsonString);
			} catch (Exception) { return null; }

		}

	}
}

using Firebase.Auth;
using Newtonsoft.Json;
using System.Text;
using TrucoRioPlatense.Services.SecureStorageHandler;

namespace TrucoRioPlatense.Models.Login {
	internal class CurrentUserStore {

		internal required UserCredential? AuthenticatedUserCredential { get; set; }

		internal async Task<string?> GetUserTokenId(FirebaseAuthClient client) {
			if (AuthenticatedUserCredential != null) {
				// Si en memoria el usuario sigue autenticado
				if (AuthenticatedUserCredential.User.Credential.IsExpired()) {
					string newToken = await AuthenticatedUserCredential.User.GetIdTokenAsync(true);
					await SSH.SetAsync(SSH.SSH_Keys_Enum.UserUid, AuthenticatedUserCredential.User.Uid);
					await SSH.SetAsync(SSH.SSH_Keys_Enum.UserTokenId, newToken);
					await SSH.SetAsync(SSH.SSH_Keys_Enum.UserTokenExpireTime, AuthenticatedUserCredential.User.Credential.Created.AddSeconds(AuthenticatedUserCredential.User.Credential.ExpiresIn).ToString());
					await SSH.SetAsync(SSH.SSH_Keys_Enum.UserRefreshToken, AuthenticatedUserCredential.User.Credential.RefreshToken);
					return newToken;
				} else {
					return AuthenticatedUserCredential.User.Credential.IdToken;
				}
			} else {
				// Ya no hay usuario en memoria
				var sshTokenExpireTime = await SSH.GetAsync(SSH.SSH_Keys_Enum.UserTokenExpireTime);

				if (!string.IsNullOrEmpty(sshTokenExpireTime) && DateTime.TryParse(sshTokenExpireTime, out DateTime expireTime)) {
					// Existe un token guardado
					var sshUserUid = await SSH.GetAsync(SSH.SSH_Keys_Enum.UserUid);
					if (DateTime.Now < expireTime) {
						// Sigue con vida el token
						return await SSH.GetAsync(SSH.SSH_Keys_Enum.UserTokenId);
					} else {
						var refreshUserToken = await SSH.GetAsync(SSH.SSH_Keys_Enum.UserRefreshToken);
						// Hay que refrescar el 
						if (string.IsNullOrEmpty(refreshUserToken))
							return null;

						var userTokenResponse = await RefreshIdTokenAsync(client, refreshUserToken);
						if (userTokenResponse.HasValue)
							return null;


						await SSH.SetAsync(SSH.SSH_Keys_Enum.UserTokenId, userTokenResponse.Value.IdToken);
						await SSH.SetAsync(SSH.SSH_Keys_Enum.UserRefreshToken, userTokenResponse.Value.RefreshToken);
						await SSH.SetAsync(SSH.SSH_Keys_Enum.UserTokenExpireTime, DateTime.Now.AddSeconds(int.Parse(userTokenResponse.Value.ExpiresIn)).ToString());
						return userTokenResponse.Value.IdToken;

					}
				} else {
					return null;
				}
			}
		}


		private async Task<TokenResponse?> RefreshIdTokenAsync(FirebaseAuthClient client, string refreshToken) {
			try {
				var _httpClient = new HttpClient();

				var requestUrl = $"https://securetoken.googleapis.com/v1/token?key=AIzaSyCbjUGYZAxrMGfEOtznq6-HMdzygniyYSk";

				var content = new StringContent(
					$"grant_type=refresh_token&refresh_token={refreshToken}",
					Encoding.UTF8,
					"application/x-www-form-urlencoded"
				);

				var response = await _httpClient.PostAsync(requestUrl, content);
				response.EnsureSuccessStatusCode();

				var jsonResponse = await response.Content.ReadAsStringAsync();
				var tokenResponse = JsonConvert.DeserializeObject<TokenResponse?>(jsonResponse);

				return tokenResponse;
			} catch (Exception ex) {
				Console.WriteLine($"Error refreshing token: {ex.Message}");
				return null;
			}
		}
		private struct TokenResponse {
			internal string IdToken { get; set; }
			internal string RefreshToken { get; set; }
			internal string ExpiresIn { get; set; }
		}
	}
}

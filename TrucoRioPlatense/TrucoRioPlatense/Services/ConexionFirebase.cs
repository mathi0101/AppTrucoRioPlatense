using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Configuration;

namespace TrucoRioPlatense.Services {
	internal class ConexionFirebase {


		private static IConfiguration _configuration;

		public static ConexionFirebase FirebaseAuthenticator;


		public ConexionFirebase(ConfigurationManager configuration) {
			_configuration = configuration;
			FirebaseAuthenticator = this;
		}

		private FirebaseAuthClient GetAuthClient() {

			var config = new FirebaseAuthConfig {
				ApiKey = _configuration["Firebase:ApiKey"],
				AuthDomain = _configuration["Firebase:AuthDomain"],
				Providers = [
					new GoogleProvider().AddScopes("email"),
					new EmailProvider()
				]
			};

			return new FirebaseAuthClient(config);
		}


		#region Publicas

		#region Log In

		public async Task<UserCredential> LogIn_UserAndPass(string email, string password) {
			UserCredential? userCredential = null;
			try {
				userCredential = await GetAuthClient().SignInWithEmailAndPasswordAsync(email, password);
				return userCredential;
			} catch (FirebaseAuthHttpException ex) {
				throw ex;
			}
		}

		public async Task<UserCredential> LogIn_Anonymus() {
			UserCredential? userCredential = null;
			try {
				userCredential = await GetAuthClient().SignInAnonymouslyAsync();
				return userCredential;
			} catch (FirebaseAuthHttpException ex) {
				throw ex;
			}
		}

		#endregion

		#region Register

		internal async Task<UserCredential> Register_UserAndPass(string email, string password, string displayName) {
			UserCredential? userCredential = null;
			try {
				userCredential = await GetAuthClient().CreateUserWithEmailAndPasswordAsync(email, password, displayName);
				return userCredential;
			} catch (FirebaseAuthHttpException ex) {
				throw ex;
			}
		}

		#endregion
		#endregion


	}
}

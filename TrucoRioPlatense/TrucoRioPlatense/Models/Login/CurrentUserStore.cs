using Firebase.Auth;

namespace TrucoRioPlatense.Models.Login {
	internal class CurrentUserStore {

		internal required UserCredential AuthenticatedUserCredential { get; set; }
	}
}

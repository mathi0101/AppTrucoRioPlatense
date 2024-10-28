namespace TrucoRioPlatense.Models.Login {
	internal class AuthLoginModels {
		public class FirebaseErrorResponse {
			public FirebaseError Error { get; set; }
		}

		public class FirebaseError {
			public int Code { get; set; }
			public string Message { get; set; }
			public List<FirebaseErrorDetail> Errors { get; set; }
		}

		public class FirebaseErrorDetail {
			public string Message { get; set; }
			public string Domain { get; set; }
			public string Reason { get; set; }
		}


	}
	internal enum Authentication_View_Response {
		Unable = -1,
		Success = 0,
		InvalidCredentials = 1,
		AccountLocked = 2,
		AccountNotFound = 3,
		TooManyAttempts = 4,
		PasswordExpired = 5,
	}
}

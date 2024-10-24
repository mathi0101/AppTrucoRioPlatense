namespace TrucoRioPlatense.Models.Login {
	internal class LoginModels {
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
}

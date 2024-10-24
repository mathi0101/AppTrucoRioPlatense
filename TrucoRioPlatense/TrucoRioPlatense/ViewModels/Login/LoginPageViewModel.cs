namespace TrucoRioPlatense.ViewModels.Login {
	internal class LoginPageViewModel : ViewModelBase {
		private int email;
		public int Email {
			get {
				return email;
			}
			set {
				email = value;
				OnPropertyChanged(nameof(Email));
			}
		}

		private int password;
		public int Password {
			get {
				return password;
			}
			set {
				password = value;
				OnPropertyChanged(nameof(Password));
			}
		}
	}
}

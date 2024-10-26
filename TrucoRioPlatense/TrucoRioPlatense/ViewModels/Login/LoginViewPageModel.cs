using Firebase.Auth;
using System.Windows.Input;
using TrucoRioPlatense.Features.Commands.Auth;
using TrucoRioPlatense.Pages;
using TrucoRioPlatense.ViewModels.Register;

namespace TrucoRioPlatense.ViewModels.Login {
	internal class LoginViewPageModel : ViewModelBase {
		private readonly Guid _id;
		private readonly FirebaseAuthClient _authClient;

		private string email;
		public string Email {
			get {
				return email;
			}
			set {
				email = value;
				OnPropertyChanged(nameof(Email));
			}
		}

		private string password;
		public string Password {
			get {
				return password;
			}
			set {
				password = value;
				OnPropertyChanged(nameof(Password));
			}
		}


		public ICommand LoginCommand { get; }
		public ICommand LoginGoogleCommand { get; }
		public ICommand LoginAppleCommand { get; }
		public ICommand NavigateToRegisterCommand { get; }


		public LoginViewPageModel(FirebaseAuthClient authClient) {
			_authClient = authClient;
			LoginCommand = new LoginCommand(this, authClient);
			NavigateToRegisterCommand = new Command(OnNavigateToRegister);

			_id = Application.Current.Id;

		}


		private async void OnNavigateToRegister() {
			// Navegar a la página de registro
			await Application.Current.MainPage.Navigation.PushAsync(new RegisterViewPage(new RegisterViewPageModel(_authClient)));
		}
	}
}

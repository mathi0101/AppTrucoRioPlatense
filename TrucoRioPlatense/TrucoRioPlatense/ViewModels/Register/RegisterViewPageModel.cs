using Firebase.Auth;
using System.Windows.Input;
using TrucoRioPlatense.Features.Commands.Auth;
using TrucoRioPlatense.Models.Login;

namespace TrucoRioPlatense.ViewModels.Register {
	internal class RegisterViewPageModel : ViewModelBase {

		private string displayName;
		public string DisplayName {
			get {
				return displayName;
			}
			set {
				displayName = value;
				OnPropertyChanged(nameof(DisplayName));
			}
		}

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

		private string confirmPassword;
		public string ConfirmPassword {
			get {
				return confirmPassword;
			}
			set {
				confirmPassword = value;
				OnPropertyChanged(nameof(ConfirmPassword));
			}
		}

		private readonly RegisterCommand _registerCommand;
		public ICommand BackToLoginCommand { get; }
		public ICommand DoRegister { get; }

		public RegisterViewPageModel(FirebaseAuthClient authClient) {
			_registerCommand = new RegisterCommand(this, authClient);
			DoRegister = new Command(ExecuteRegister);
			BackToLoginCommand = new Command(OnBackToLoginCommand);
		}


		private async void ExecuteRegister() {
			var result = await _registerCommand.ExecuteWithResultAsync(null);

			// Manejo del resultado del RegisterCommand
			if (result == Authentication_View_Response.Success) {
				// Registro exitoso: hacer algo más aquí
				await Application.Current.MainPage.DisplayAlert("Éxito", "Registro completado", "OK");

				// Navegar hacia otra página o realizar acciones adicionales
				await Application.Current.MainPage.Navigation.PopAsync();
			} else {
				// Mostrar errores si hubo un problema
				await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error en el registro", "OK");

				// Acciones adicionales en caso de error, si es necesario
			}
		}

		private async void OnBackToLoginCommand() {
			await Application.Current.MainPage.Navigation.PopAsync(true);
		}


	}
}

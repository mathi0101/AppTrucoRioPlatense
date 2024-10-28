using Firebase.Auth;
using System.Windows.Input;
using TrucoRioPlatense.Features.Commands.Auth;
using TrucoRioPlatense.Models.Login;
using TrucoRioPlatense.Services.Sqlite3;

namespace TrucoRioPlatense.ViewModels.Register {
	internal class RegisterViewPageModel : ViewModelBase {

		#region Propiedades

		#region Privadas
		private readonly RegisterCommand _registerCommand;

		private string displayName;
		private string email;
		private string password;
		private string confirmPassword;

		#endregion

		#region Publicas
		public ICommand BackToLoginCommand { get; }
		public ICommand DoRegister { get; }

		public string DisplayName {
			get {
				return displayName;
			}
			set {
				displayName = value;
				OnPropertyChanged(nameof(DisplayName));
			}
		}

		public string Email {
			get {
				return email;
			}
			set {
				email = value;
				OnPropertyChanged(nameof(Email));
			}
		}

		public string Password {
			get {
				return password;
			}
			set {
				password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

		public string ConfirmPassword {
			get {
				return confirmPassword;
			}
			set {
				confirmPassword = value;
				OnPropertyChanged(nameof(ConfirmPassword));
			}
		}
		#endregion

		#endregion

		#region Constructor


		public RegisterViewPageModel(FirebaseAuthClient authClient, CurrentUserStore userStore, SQLiteDB _dbConnection) {
			_registerCommand = new RegisterCommand(this, authClient, userStore, _dbConnection);
			DoRegister = new Command(ExecuteRegister);
			BackToLoginCommand = new Command(OnBackToLoginCommand);
		}
		#endregion

		#region Eventos
		public event EventHandler RegistrationCompleted;
		#endregion

		#region Metodos
		#region Privados
		private async void ExecuteRegister() {
			if (_registerCommand.CanExecute(null) && ValidateFields()) {

				var result = await _registerCommand.ExecuteWithResultAsync(null);


				if (result == Authentication_View_Response.Success) {
					RegistrationCompleted?.Invoke(this, EventArgs.Empty);

					await Application.Current.MainPage.DisplayAlert("Éxito", "Registro completado", "OK");

					await Application.Current.MainPage.Navigation.PopAsync();
				} else {

					await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error en el registro", "OK");


				}
			}
		}

		private bool ValidateFields() {
			string[] fields = [displayName, email, password, confirmPassword];

			string ErrorMessage = string.Empty;

			foreach (var field in fields) {
				if (string.IsNullOrWhiteSpace(field)) {
					ErrorMessage = "Campo vacío.";
					return false;
				}
			}


			if (password != confirmPassword) {
				//await Application.Current.MainPage.DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
				//btnConfirm.Focus();
				return false;
			}

			return true;
		}

		private async void OnBackToLoginCommand() {
			await Application.Current.MainPage.Navigation.PopAsync(true);
		}

		#endregion
		#endregion


	}
}

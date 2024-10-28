using Firebase.Auth;
using System.Windows.Input;
using TrucoRioPlatense.Features.Commands.Auth;
using TrucoRioPlatense.Models.Login;
using TrucoRioPlatense.Pages;
using TrucoRioPlatense.Services.Sqlite3;
using TrucoRioPlatense.ViewModels.Register;

namespace TrucoRioPlatense.ViewModels.Login {
	internal class LoginViewPageModel : ViewModelBase {

		#region Propiedades

		#region Privadas

		private readonly LoginCommand _loginCommand;
		private readonly FirebaseAuthClient _authClient;
		private readonly CurrentUserStore _currentUserStore;
		private readonly SQLiteDB _dbConnection;

		private string email;
		private string password;

		private readonly Guid _id;

		#endregion

		#region Publicas

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


		public ICommand LoginGoogleCommand { get; }
		public ICommand LoginAppleCommand { get; }
		public ICommand NavigateToRegisterCommand { get; }
		public ICommand DoLogin { get; }

		#endregion

		#endregion

		#region Constructor

		public LoginViewPageModel(FirebaseAuthClient authClient, CurrentUserStore userStore, SQLiteDB dbConnection) {
			_authClient = authClient;
			_currentUserStore = userStore;
			_loginCommand = new LoginCommand(this, authClient, userStore, dbConnection);
			_dbConnection = dbConnection;

			NavigateToRegisterCommand = new Command(OnNavigateToRegister);
			DoLogin = new Command(ExecuteLogin);

			_id = Application.Current.Id;
		}


		#endregion

		#region Metodos

		#region Privados
		private async void ExecuteLogin() {
			if (_loginCommand.CanExecute(null) && ValidateLogin()) {


				var result = await _loginCommand.ExecuteWithResultAsync(FirebaseProviderType.EmailAndPassword);




				if (result == Authentication_View_Response.Success) {

					await Application.Current.MainPage.DisplayAlert("Éxito", "Bienvenido!", "OK");

					Application.Current.MainPage = new MainPage();
				} else {

					await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error en el registro", "OK");


				}
			}
		}

		private bool ValidateLogin() {
			return true;
		}

		private async void OnNavigateToRegister() {

			RegisterViewPageModel registerModel = new RegisterViewPageModel(_authClient, _currentUserStore, _dbConnection);

			registerModel.RegistrationCompleted += RegisterModel_RegistrationCompleted;

			await Application.Current.MainPage.Navigation.PushAsync(new RegisterViewPage(registerModel));
		}

		private void RegisterModel_RegistrationCompleted(object? sender, EventArgs e) {
			ExecuteLogin();
		}
		#endregion

		#endregion
	}
}

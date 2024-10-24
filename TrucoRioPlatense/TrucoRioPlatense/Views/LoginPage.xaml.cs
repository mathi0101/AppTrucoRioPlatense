using TrucoRioPlatense.ViewModels.Login;

namespace TrucoRioPlatense.Views {
	public partial class LoginPage : ContentPage {
		public LoginPage() {
			InitializeComponent();

			var window = Application.Current.Windows.FirstOrDefault();
			if (window != null) {
				window.Width = 360;
				window.Height = 640;
			}
		}

		private async void OnLoginClicked(object sender, EventArgs e) {
			var email = EmailEntry.Text;
			var password = PasswordEntry.Text;

			var auth = new AppAuthentication(Application.Current.Id);
			try {

				var response = await auth.LogInAsync(email, password);

				switch (response) {
					case AppAuthentication.Authentication_View_Response.Success:
					await DisplayAlert("Login exitoso", "Bienvenido!", "OK");
					// Aqu� puedes redirigir al usuario a la p�gina principal
					Application.Current.MainPage = new MainPage();
					break;

					case AppAuthentication.Authentication_View_Response.InvalidCredentials:
					await DisplayAlert("Credenciales inv�lidas", "El correo o la contrase�a que ingresaste son incorrectos.", "OK");
					break;

					case AppAuthentication.Authentication_View_Response.AccountLocked:
					await DisplayAlert("Cuenta bloqueada", "Tu cuenta ha sido bloqueada temporalmente por motivos de seguridad.", "OK");
					break;

					case AppAuthentication.Authentication_View_Response.AccountNotFound:
					bool register = await DisplayAlert("Cuenta no encontrada", "No se ha encontrado una cuenta con este correo. �Deseas registrarte?", "OK", "Cancelar");
					if (register) {
						// Si elige registrarse, pedir el nombre de usuario
						string username = await DisplayPromptAsync("Nombre de usuario", "Por favor, ingresa tu nombre de usuario para el juego:", "OK", "Cancelar", "Tu nombre de usuario", 20, Keyboard.Text);
						if (!string.IsNullOrEmpty(username) && username.Length > 4) {
							// Aqu� ejecutas el m�todo de registro pasando tambi�n el nombre de usuario
							var registerRes = await auth.RegisterUserAsync(email, password, username);

							if (registerRes) {
								await DisplayAlert("Registro exitoso", $"Bienvenido {username}!", "OK");
								// Aqu� puedes redirigir al usuario a la p�gina principal
								Application.Current.MainPage = new MainPage();
							} else {
								await DisplayAlert("Atenci�n!", $"Error al registrar", "OK");
							}
						} else {
							await DisplayAlert("Atenci�n!", $"Usuario no admitido", "OK");
						}
					}
					break;

					case AppAuthentication.Authentication_View_Response.TooManyAttempts:
					await DisplayAlert("Demasiados intentos", "Has realizado demasiados intentos fallidos. Por favor, espera e intenta nuevamente m�s tarde.", "OK");
					break;

					case AppAuthentication.Authentication_View_Response.PasswordExpired:
					await DisplayAlert("Contrase�a expirada", "Tu contrase�a ha expirado. Por favor, restabl�cela.", "OK");
					// Aqu� podr�as redirigir al usuario a una p�gina de restablecimiento de contrase�a
					break;

					case AppAuthentication.Authentication_View_Response.Unable:
					await DisplayAlert("Error", "No se ha podido realizar el login. Intenta de nuevo m�s tarde.", "OK");
					break;

					default:
					await DisplayAlert("Error", "Ha ocurrido un error inesperado.", "OK");
					break;
				}

			} catch (Exception ex) {
				await DisplayAlert("Error", ex.Message, "OK");
			}
		}


		private async void OnLoginAnonymouslyClicked(object sender, EventArgs e) {
			// L�gica para iniciar sesi�n an�nimamente
			// Agrega tu l�gica de autenticaci�n an�nima aqu�

			await DisplayAlert("Login", "Iniciar sesi�n como an�nimo clicado", "OK");
		}
		private async void OnLoginWithGoogleClicked(object sender, EventArgs e) {
			// L�gica para iniciar sesi�n con Google
			await DisplayAlert("Login", "Iniciar sesi�n con Google clicado", "OK");
		}

		private async void OnLoginWithAppleClicked(object sender, EventArgs e) {
			// L�gica para iniciar sesi�n con Apple
			await DisplayAlert("Login", "Iniciar sesi�n con Apple clicado", "OK");
		}
	}

}
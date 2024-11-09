using Firebase.Auth;
using TrucoRioPlatense.Models.Login;
using TrucoRioPlatense.Services.Sqlite3;

namespace TrucoRioPlatense.ViewModels.MainMenu {
	internal class MainMenuViewModel : ViewModelBase {

		public MainMenuViewModel(FirebaseAuthClient authClient, CurrentUserStore currentUserStore, SQLiteDB dbConnection) {



		}
	}
}

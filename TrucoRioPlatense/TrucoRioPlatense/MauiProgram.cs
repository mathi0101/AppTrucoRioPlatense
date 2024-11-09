using Firebase.Auth;
using Firebase.Auth.Providers;
using TrucoRioPlatense.Data.MauiBuilder;
using TrucoRioPlatense.Models.Login;
using TrucoRioPlatense.Pages;
using TrucoRioPlatense.Services.Sqlite3;
using TrucoRioPlatense.ViewModels.Login;
using TrucoRioPlatense.ViewModels.MainMenu;
using TrucoRioPlatense.ViewModels.Register;

namespace TrucoRioPlatense {
	public static class MauiProgram {
		public static MauiApp CreateMauiApp() {
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts => {
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});


			// Loading Page
			builder.Services.AddSingleton<LoadingPageViewModel>();
			builder.Services.AddSingleton(s => new LoadingPage(s.GetRequiredService<LoadingPageViewModel>()));
			// Login Page
			builder.Services.AddSingleton<LoginViewPageModel>();
			builder.Services.AddSingleton(s => new LoginViewPage(s.GetRequiredService<LoginViewPageModel>()));
			// Register Page
			builder.Services.AddTransient<RegisterViewPageModel>();
			builder.Services.AddTransient(s => new RegisterViewPage(s.GetRequiredService<RegisterViewPageModel>()));

			// Main Menu Page
			builder.Services.AddSingleton<MainMenuViewModel>();
			builder.Services.AddSingleton(s => new MainMenuPage(s.GetRequiredService<MainMenuViewModel>()));



			builder.Services.AddSingleton<CurrentUserStore>();

			AppCredentialsLoader.LoadCredentials(builder);


			// FirebaseAuthentication
			builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig() {
				ApiKey = builder.Configuration["Firebase:ApiKey"],
				AuthDomain = builder.Configuration["Firebase:AuthDomain"],
				Providers = [
					new EmailProvider(),
					new GoogleProvider()
				]
			}));

			builder.Services.AddSingleton(new SQLiteDB("TrucoRioPlatense.db"));


			return builder.Build();
		}
	}
}

using Firebase.Auth;
using Firebase.Auth.Providers;
using TrucoRioPlatense.Data.MauiBuilder;
using TrucoRioPlatense.Models.Login;
using TrucoRioPlatense.Pages;
using TrucoRioPlatense.Services.Sqlite3;
using TrucoRioPlatense.ViewModels.Login;
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
			builder.Services.AddSingleton<LoadingPage>(s => new LoadingPage(s.GetRequiredService<LoadingPageViewModel>()));
			// Login Page
			builder.Services.AddSingleton<LoginViewPageModel>();
			builder.Services.AddSingleton<LoginViewPage>(s => new LoginViewPage(s.GetRequiredService<LoginViewPageModel>()));
			// Register Page
			builder.Services.AddTransient<RegisterViewPageModel>();
			builder.Services.AddTransient<RegisterViewPage>(s => new RegisterViewPage(s.GetRequiredService<RegisterViewPageModel>()));

			// Main Page
			builder.Services.AddSingleton<MainPage>();



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

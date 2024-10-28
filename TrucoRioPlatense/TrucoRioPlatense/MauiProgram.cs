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


			// LoadingPage
			builder.Services.AddTransient<LoadingPageViewModel>();
			builder.Services.AddTransient<LoadingPage>(s => new LoadingPage(s.GetRequiredService<LoadingPageViewModel>()));
			// Login Page
			builder.Services.AddTransient<LoginViewPageModel>();
			builder.Services.AddTransient<LoginViewPage>(s => new LoginViewPage(s.GetRequiredService<LoginViewPageModel>()));
			// Register View
			builder.Services.AddTransient<RegisterViewPageModel>();
			builder.Services.AddTransient<RegisterViewPage>(s => new RegisterViewPage(s.GetRequiredService<RegisterViewPageModel>()));



			builder.Services.AddSingleton<CurrentUserStore>();

			AppCredentialsLoader.LoadCredentials(builder);


			// FirebaseAuthentication
			builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig() {
				ApiKey = builder.Configuration["Firebase:ApiKey"],
				AuthDomain = builder.Configuration["Firebase:AuthDomain"],
				Providers = [
					new EmailProvider()
				]
			}));

			builder.Services.AddSingleton(new SQLiteDB("TrucoRioPlatense.db"));


			return builder.Build();
		}
	}
}

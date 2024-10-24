using Microsoft.Extensions.Configuration;
using TrucoRioPlatense.Services;

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




			builder.Configuration.AddJsonFile("appSettings.json", false, true);

			new ConexionFirebase(builder.Configuration);


			return builder.Build();
		}
	}
}

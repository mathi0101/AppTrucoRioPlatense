using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace TrucoRioPlatense.Data.MauiBuilder {
	internal static class AppCredentialsLoader {
		private const string USER_SECRETS_ID = "AppSecrets";
		public static void LoadCredentials(MauiAppBuilder builder) {

			string configPath;

#if ANDROID
			configPath= Path.Combine(FileSystem.Current.AppDataDirectory, "appSettings.json");
			var assembly = Assembly.GetExecutingAssembly();
			using (Stream stream = assembly.GetManifestResourceStream("TrucoRioPlatense.Credentials.appSettings.json"))
			using (FileStream fileStream = File.Create(configPath)) {
				stream.CopyTo(fileStream);
			}
#else
			configPath = "Credentials/appSettings.json";
#endif
			try {
				//builder.Configuration.AddJsonFile(configPath, optional: false, reloadOnChange: true); // Carga la configuracion desde un json local (PELIGROSO)

				// Mejor usemos User Secrets

				builder.Configuration.AddUserSecrets(USER_SECRETS_ID);

			} catch (Exception ex) {
				throw ex;
			}
		}
	}
}

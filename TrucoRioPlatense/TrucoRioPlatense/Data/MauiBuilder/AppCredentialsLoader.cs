using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace TrucoRioPlatense.Data.MauiBuilder {
	internal static class AppCredentialsLoader {
		private const string USER_SECRETS_ID = "AppSecrets";
		public static void LoadCredentials(MauiAppBuilder builder) {

			string configPath;

			try {
				//builder.Configuration.AddJsonFile(configPath, optional: false, reloadOnChange: true); // Carga la configuracion desde un json local (PELIGROSO)

				// Mejor usemos User Secrets
#if ANDROID
				configPath = Path.Combine(FileSystem.Current.AppDataDirectory, "appSettings.json");
				var assembly = Assembly.GetExecutingAssembly();
				using (Stream stream = assembly.GetManifestResourceStream("TrucoRioPlatense.Credentials.appSettings.json"))
				using (FileStream fileStream = File.Create(configPath)) {
					stream.CopyTo(fileStream);
				}
				builder.Configuration.AddJsonFile(configPath, optional: false, reloadOnChange: true);
#else
				//configPath = "Credentials/appSettings.json";
				builder.Configuration.AddUserSecrets(USER_SECRETS_ID);
#endif


			} catch (Exception ex) {
				throw ex;
			}
		}
	}
}

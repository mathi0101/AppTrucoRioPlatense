using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace TrucoRioPlatense.Data.MauiBuilder {
	internal static class AppCredentialsLoader {
		public static void LoadCredentials(MauiAppBuilder builder) {

			string configPath;

#if ANDROID
			configPath= Path.Combine(FileSystem.Current.AppDataDirectory, "appSettings.json");
			var assembly = Assembly.GetExecutingAssembly();
			using (Stream stream = assembly.GetManifestResourceStream("TrucoRioPlatense.appSettings.json"))
			using (FileStream fileStream = File.Create(configPath)) {
				stream.CopyTo(fileStream);
			}
#else
			configPath = "appSettings.json";
#endif
			try {
				builder.Configuration.AddJsonFile(configPath, optional: false, reloadOnChange: true);
			} catch (Exception ex) {
				throw ex;
			}
		}
	}
}

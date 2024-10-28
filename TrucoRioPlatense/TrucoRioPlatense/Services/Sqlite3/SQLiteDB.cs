using SQLite;
using TrucoRioPlatense.Models.LocalDatabase;

namespace TrucoRioPlatense.Services.Sqlite3 {
	internal class SQLiteDB {

		#region Propiedades

		#region Privadas

		private readonly SQLiteAsyncConnection _conn;

		public SQLiteAsyncConnection Connection => _conn;


		#endregion

		#endregion

		#region Constructor

		public SQLiteDB(string dbName) {
			string databasePath = GetDatabasePath(dbName);
			string pass = "contra";
			var connectionString = $"Data Source={databasePath};Password={pass};";
			_conn = new SQLiteAsyncConnection(databasePath);
		}
		#endregion

		#region Metodos
		#region Privados
		private string GetDatabasePath(string dbName) {
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			return Path.Combine(folderPath, dbName);
		}
		#endregion
		#region Publicos
		internal async Task PreloadDatabase() {
			var result = await _conn.CreateTablesAsync(CreateFlags.ImplicitPK, typeof(UserAccounts));

			;
		}


		#endregion
		#endregion
	}
}

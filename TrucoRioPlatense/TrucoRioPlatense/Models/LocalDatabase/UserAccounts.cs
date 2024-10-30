using Firebase.Auth;
using SQLite;
using System.Linq.Expressions;
using TrucoRioPlatense.Services.Sqlite3;

namespace TrucoRioPlatense.Models.LocalDatabase {
	internal class UserAccounts {

		[AutoIncrement]
		public int Id { get; set; }
		public string Uid { get; set; }
		public string? DisplayName { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }


		public UserAccounts() { }

		private void Cargar(UserAccounts obj) {
			Id = obj.Id;
			Uid = obj.Uid;
			DisplayName = obj.DisplayName;
			Email = obj.Email;
			FirstName = obj.FirstName;
			LastName = obj.LastName;
		}
		public void CargarNuevoUsuario(UserCredential auth) {
			Uid = auth.User.Uid;
			DisplayName = auth.User.Info.DisplayName;
			Email = auth.User.Info.Email;
			FirstName = auth.User.Info.FirstName;
			LastName = auth.User.Info.LastName;
		}

		public async Task<bool> GetUserAsync(SQLiteDB connection, Expression<Func<UserAccounts, bool>> func) {
			var res = await connection.Connection.FindAsync(func);

			if (res != null) {
				Cargar(res);
				return true;
			}

			return false;
		}

		public async Task<bool> InsertAsync(SQLiteDB connection) {
			var res = await connection.Connection.InsertAsync(this);

			return res > 0;
		}

		public async Task<bool> UpdateAsync(SQLiteDB connection) {
			var res = await connection.Connection.UpdateAsync(this, typeof(UserAccounts));

			return res > 0;
		}


		public async Task<bool> DeleteAsync(SQLiteDB connection) {
			var res = await connection.Connection.DeleteAsync(this);

			return res > 0;
		}
	}
}

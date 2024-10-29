using Firebase.Auth;
using SQLite;
using System.Linq.Expressions;
using TrucoRioPlatense.Services.Sqlite3;

namespace TrucoRioPlatense.Models.LocalDatabase {
	internal class UserAccounts {

		[AutoIncrement]
		public int Id { get; set; }
		public string PlayerID { get; set; }
		public string? DisplayName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string? Token { get; set; }
		public DateTime? TokenExpireDate { get; set; }


		public UserAccounts() { }

		private void Cargar(UserAccounts obj) {
			Id = obj.Id;
			PlayerID = obj.PlayerID;
			DisplayName = obj.DisplayName;
			Email = obj.Email;
			Password = obj.Password;
			Token = obj.Token;
			TokenExpireDate = obj.TokenExpireDate;
		}
		public void CargarNuevoUsuario(UserCredential auth) {
			PlayerID = auth.User.Uid;
			DisplayName = auth.User.Info.DisplayName;
			Token = auth.User.Credential.IdToken;
			TokenExpireDate = auth.User.Credential.Created.AddSeconds(auth.User.Credential.ExpiresIn);
			Email = auth.User.Info.Email;
			Password = "";
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

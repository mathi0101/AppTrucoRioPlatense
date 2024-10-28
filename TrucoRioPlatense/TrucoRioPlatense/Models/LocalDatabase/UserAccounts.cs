using SQLite;

namespace TrucoRioPlatense.Models.LocalDatabase {
	internal class UserAccounts {

		[AutoIncrement]
		public int Id { get; set; }
		public string PlayerID { get; set; }
		public string DisplayName { get; set; }
		public string Email { get; set; }
		public bool IsConnected { get; set; }
	}
}

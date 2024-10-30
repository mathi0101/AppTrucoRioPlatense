using Newtonsoft.Json;

namespace TrucoRioPlatense.Services.SecureStorageHandler {
	internal static class SSH {

		private static readonly ISecureStorage _secure = SecureStorage.Default;


		public enum SSH_Keys_Enum {
			UserUid,
			UserTokenId,
			UserTokenExpireTime,
			UserRefreshToken
		}

		#region SETASYNC

		public static async Task<bool> SetAsync(SSH_Keys_Enum key, string value) {
			return await SetAsync(key.ToString(), value);

		}

		public static async Task<bool> SetAsync(string key, string value) {
			try {
				await _secure.SetAsync(key, value);
				return true;
			} catch (Exception ex) {

				return false;
			}

		}
		public static async Task<bool> SetAsync<T>(string key, T value) where T : new() {
			try {
				var jsonString = JsonConvert.SerializeObject(value);

				await _secure.SetAsync(key, jsonString);
				return true;
			} catch (Exception ex) {

				return false;
			}

		}

		#endregion

		#region GETASYNC

		public static async Task<string?> GetAsync(SSH_Keys_Enum key) {
			return await GetAsync(key.ToString());

		}
		public static async Task<string?> GetAsync(string key) {
			try {
				return await _secure.GetAsync(key);
			} catch (Exception ex) {

				return null;
			}
		}

		public static async Task<T?> GetAsync<T>(string key) where T : new() {
			try {
				return JsonConvert.DeserializeObject<T>(await _secure.GetAsync(key));
			} catch (Exception ex) {

				return default(T);
			}
		}

		#endregion


	}
}

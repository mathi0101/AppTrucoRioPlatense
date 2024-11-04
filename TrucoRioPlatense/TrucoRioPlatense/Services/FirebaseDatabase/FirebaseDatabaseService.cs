using System.Net.Http.Json;

namespace TrucoRioPlatense.Services.FirebaseDatabase {

	public class FirebaseDatabaseService {
		private readonly HttpClient _httpClient;
		private readonly string _firebaseDatabaseUrl;
		private string _idToken;

		public FirebaseDatabaseService(string firebaseDatabaseUrl, HttpClient httpClient) {
			_firebaseDatabaseUrl = firebaseDatabaseUrl;
			_httpClient = httpClient;
		}

		// Configura el id_token del usuario actual
		public void SetIdToken(string idToken) {
			_idToken = idToken;
		}

		// Método para obtener datos
		public async Task<T?> GetDataAsync<T>(string path) {
			var url = $"{_firebaseDatabaseUrl}/{path}.json?auth={_idToken}";
			return await _httpClient.GetFromJsonAsync<T>(url);
		}

		// Método para guardar datos
		public async Task<bool> SaveDataAsync<T>(string path, T data) {
			var url = $"{_firebaseDatabaseUrl}/{path}.json?auth={_idToken}";
			var response = await _httpClient.PutAsJsonAsync(url, data);
			return response.IsSuccessStatusCode;
		}

		// Método para actualizar datos parcialmente
		public async Task<bool> UpdateDataAsync<T>(string path, T data) {
			var url = $"{_firebaseDatabaseUrl}/{path}.json?auth={_idToken}";
			var response = await _httpClient.PatchAsync(url, JsonContent.Create(data));
			return response.IsSuccessStatusCode;
		}

		// Método para eliminar datos
		public async Task<bool> DeleteDataAsync(string path) {
			var url = $"{_firebaseDatabaseUrl}/{path}.json?auth={_idToken}";
			var response = await _httpClient.DeleteAsync(url);
			return response.IsSuccessStatusCode;
		}
	}

}

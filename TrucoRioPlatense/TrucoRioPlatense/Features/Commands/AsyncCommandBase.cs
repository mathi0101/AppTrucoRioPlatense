using Firebase.Auth;
using System.Windows.Input;
using TrucoRioPlatense.Helpers;
using TrucoRioPlatense.Models.Login;

namespace TrucoRioPlatense.Features.Commands {
	internal abstract class AsyncCommandBase(Action<Exception>? onException = null) : ICommand {

		private readonly Action<Exception>? _onException = onException;

		private bool _isExecuting;
		public bool IsExecuting {
			get {
				return _isExecuting;
			}
			set {
				_isExecuting = value;
				OnCanExecuteChanged();
			}
		}

		public event EventHandler? CanExecuteChanged;


		public bool CanExecute(object? parameter) => !IsExecuting;
		protected void OnCanExecuteChanged() {
			CanExecuteChanged?.Invoke(this, new EventArgs());
		}


		public async void Execute(object? parameter) {
			IsExecuting = true;


			try {
				await ExecuteAsync(parameter);
			} catch (Exception ex) {
				_onException?.Invoke(ex);
			}

			IsExecuting = false;
		}
		public async Task<AsyncCommandBaseResult> ExecuteWithResultAsync(object? parameter) {
			IsExecuting = true;
			AsyncCommandBaseResult response = default;
			try {
				response = await ExecuteAsync(parameter);
			} catch (Exception ex) {
				_onException?.Invoke(ex);
				response = new AsyncCommandBaseResult(ex as FirebaseAuthHttpException);
			}
			IsExecuting = false;
			return response;
		}
		protected abstract Task<AsyncCommandBaseResult> ExecuteAsync(object? parameter);




	}

	public struct AsyncCommandBaseResult {

		public Authentication_View_Response Value { get; set; }
		public FirebaseAuthHttpException? Error { get; set; }
		public AsyncCommandBaseResult(FirebaseAuthHttpException? error) {
			Error = error;

			Value = FirebaseHelper.ReturnUserAuthResponse(error);
		}
		public AsyncCommandBaseResult(Authentication_View_Response value, FirebaseAuthHttpException? error) {
			Value = value;
			Error = error;

		}

		public static readonly AsyncCommandBaseResult Unable = new AsyncCommandBaseResult(Authentication_View_Response.Unable, null);
		public static readonly AsyncCommandBaseResult Success = new AsyncCommandBaseResult(Authentication_View_Response.Success, null);
	}
}

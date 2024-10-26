using System.Windows.Input;

namespace TrucoRioPlatense.Features.Commands {
	internal abstract class AsyncCommandBase<T>(Action<Exception>? onException = null) : ICommand {

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


		public async void Execute(object? parameter) {
			IsExecuting = true;


			try {
				await ExecuteAsync(parameter);
			} catch (Exception ex) {
				_onException?.Invoke(ex);
			}

			IsExecuting = false;
		}
		public async Task<T> ExecuteWithResultAsync(object? parameter) {
			IsExecuting = true;
			T response = default;
			try {
				response = await ExecuteAsync(parameter);
			} catch (Exception ex) {
				_onException?.Invoke(ex);
			}
			IsExecuting = false;
			return response;
		}
		protected abstract Task<T> ExecuteAsync(object? parameter);


		protected void OnCanExecuteChanged() {
			CanExecuteChanged?.Invoke(this, new EventArgs());
		}


	}
}

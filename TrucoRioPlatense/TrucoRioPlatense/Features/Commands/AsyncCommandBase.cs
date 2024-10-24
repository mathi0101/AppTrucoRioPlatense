using System.Windows.Input;

namespace TrucoRioPlatense.Features.Commands {
	internal abstract class AsyncCommandBase : ICommand {

		private readonly Action<Exception> _onException;

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

		public AsyncCommandBase(Action<Exception> onException = null) {
			_onException = onException;
		}

		public event EventHandler CanExecuteChanged;


		public bool CanExecute(object parameter) {
			return !IsExecuting;
		}


		public async void Execute(object parameter) {
			IsExecuting = true;

			try {
				await ExecuteAsync(parameter);
			} catch (Exception ex) {
				_onException?.Invoke(ex);
			}

			IsExecuting = false;
		}

		protected abstract Task ExecuteAsync(object parameter);
		internal abstract Task InvokeAsync(object parameter);


		protected void OnCanExecuteChanged() {
			CanExecuteChanged?.Invoke(this, new EventArgs());
		}


	}
}

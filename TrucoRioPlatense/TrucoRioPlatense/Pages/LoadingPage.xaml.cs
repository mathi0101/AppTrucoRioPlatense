using TrucoRioPlatense.ViewModels.Register;

namespace TrucoRioPlatense.Pages {

	public partial class LoadingPage : ContentPage {

		private LoadingPageViewModel? _model { get => BindingContext as LoadingPageViewModel; }

		public LoadingPage(object bindingContext) {
			InitializeComponent();

			BindingContext = bindingContext;

#if WINDOWS

			var window = Application.Current.Windows.FirstOrDefault();
			if (window != null) {
				window.Width = 360;
				window.Height = 640;
			}
#endif
		}

		protected override async void OnAppearing() {
			base.OnAppearing();

			if (_model != null)
				await _model.StartApplicaction();
		}
	}
}
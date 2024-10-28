using TrucoRioPlatense.ViewModels.Register;

namespace TrucoRioPlatense.Pages {

	public partial class LoadingPage : ContentPage {

		private LoadingPageViewModel _model { get => BindingContext as LoadingPageViewModel; }

		public LoadingPage(object bindingContext) {
			InitializeComponent();

			BindingContext = bindingContext;
		}

		protected override async void OnAppearing() {
			base.OnAppearing();

			await _model.LoadDataAsync();
		}
	}
}
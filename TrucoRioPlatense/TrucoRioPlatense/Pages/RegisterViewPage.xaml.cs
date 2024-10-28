using TrucoRioPlatense.ViewModels.Register;

namespace TrucoRioPlatense.Pages {
	public partial class RegisterViewPage : ContentPage {


		private RegisterViewPageModel _model { get => BindingContext as RegisterViewPageModel; }

		public RegisterViewPage(object bindingContext) {
			InitializeComponent();


			BindingContext = bindingContext;

			var window = Application.Current?.Windows.FirstOrDefault();
			if (window != null) {
				window.Width = 360;
				window.Height = 640;
			}
		}
	}

}
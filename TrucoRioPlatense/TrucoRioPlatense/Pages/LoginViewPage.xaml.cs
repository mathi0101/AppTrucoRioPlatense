namespace TrucoRioPlatense.Pages {
	public partial class LoginViewPage : ContentPage {
		public LoginViewPage(object bindingContext) {
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


	}

}
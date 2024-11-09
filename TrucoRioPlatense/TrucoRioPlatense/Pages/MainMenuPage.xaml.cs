using TrucoRioPlatense.ViewModels.MainMenu;

namespace TrucoRioPlatense.Pages;

public partial class MainMenuPage : ContentPage {

	private MainMenuViewModel? _model { get => BindingContext as MainMenuViewModel; }

	public MainMenuPage(object bindingContext) {
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


	protected override void OnAppearing() {

		base.OnAppearing();
	}
}
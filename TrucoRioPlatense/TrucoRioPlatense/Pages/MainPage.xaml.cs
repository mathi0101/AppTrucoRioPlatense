namespace TrucoRioPlatense.Pages {
	public partial class MainPage : ContentPage {
		int count = 0;

		public MainPage() {
			InitializeComponent();

			SetWindowSize(360, 640);

		}

		private void OnCounterClicked(object sender, EventArgs e) {
			count++;

			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";

			SemanticScreenReader.Announce(CounterBtn.Text);
		}

		private void SetWindowSize(int width, int height) {
			var window = Application.Current.Windows.FirstOrDefault();
			if (window != null) {
				window.Width = width;
				window.Height = height;
			}
		}
	}

}

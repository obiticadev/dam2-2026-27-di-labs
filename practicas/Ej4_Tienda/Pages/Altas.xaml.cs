namespace Ej4_Tienda.Pages;

public partial class Altas : ContentPage
{
	public Altas()
	{
		InitializeComponent();
	}

	private void OnEntryHandlerChanged(object? sender, EventArgs e)
	{
		if (sender is not Entry entry)
			return;

		// Quitamos el borde nativo solo en los campos de esta página.
#if WINDOWS
		if (entry.Handler?.PlatformView is Microsoft.UI.Xaml.Controls.TextBox textBox)
		{
			textBox.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
			textBox.Resources["TextControlBorderThemeThicknessFocused"] = new Microsoft.UI.Xaml.Thickness(0);
			textBox.Padding = new Microsoft.UI.Xaml.Thickness(0);
		}
#elif ANDROID
		if (entry.Handler?.PlatformView is Android.Widget.EditText editText)
		{
			editText.Background = null;
			editText.SetPadding(0, 0, 0, 0);
		}
#elif IOS || MACCATALYST
		if (entry.Handler?.PlatformView is UIKit.UITextField textField)
			textField.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
	}
}

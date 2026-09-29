namespace Ej3_Login
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Verificamos que ambos campos NO estén vacíos ni solo con espacios
            if (!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(pass.Text))
            {
                await DisplayAlert("Éxito", $"Bienvenido, {name.Text}!", "Aceptar");
                name.Text = string.Empty;
                pass.Text = string.Empty;                
            }
            else
            {
                await DisplayAlert("Aviso", "Por favor, rellena todos los campos.", "OK");
            }
        }
    }
}

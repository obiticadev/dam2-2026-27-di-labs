
namespace HolaMundo
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button botonPulsado)
            {
                botonPulsado.Text = "¡Me has \npulsado!";
                botonPulsado.Background = Colors.Green;
            }
            count++;
            if ((contador.Text = count.ToString()) == "1")
            {
                contador.Text = "1 clic";
            } else
            {
                contador.Text = $"{count} clics";
            }
        }
    }
}

namespace trivial
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            
        }
        private async void capitales_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Capitales());
        }

        private async void paises_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Paises());
        }
    }
}

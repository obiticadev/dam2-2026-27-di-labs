using System.Collections.ObjectModel;

namespace Ej3_Login
{
    public partial class MainPage : ContentPage
    {
        // ObservableCollection notifica a la interfaz automáticamente cada vez que se agrega un elemento
        ObservableCollection<User> list = new();

        public MainPage()
        {
            InitializeComponent();

            // Vinculamos la lista de datos al control CollectionView
            listaUsuarios.ItemsSource = list;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Verificamos que ambos campos NO estén vacíos ni solo con espacios
            if (!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(pass.Text))
            {
                User user = new(name.Text, pass.Text);
                await DisplayAlert("Éxito", $"Bienvenido, {name.Text}!", "Aceptar");

                // Con solo agregarlo a la lista, el CollectionView se actualiza en pantalla solo
                list.Add(user);

                name.Text = string.Empty;
                pass.Text = string.Empty;                
            }
            else
            {
                await DisplayAlert("Aviso", "Por favor, rellena todos los campos.", "OK");
            }
        }
    }

    public class User
    {
        public string name { get; set; }
        public string pass { get; set; }

        public User(string name, string pass)
        {
            this.name = name;
            this.pass = pass;
        }
    }
}

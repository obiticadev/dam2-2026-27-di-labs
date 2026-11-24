namespace Ej4_Tienda
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Al arrancar, se cargan los clientes guardados en datosClientes.txt (AppData)
            Models.ServicioClientes.Cargar();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
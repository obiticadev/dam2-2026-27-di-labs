namespace Actividad1_Calculadora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            Entrada.Text = "0";
        }

        private void EqualsButton_Clicked(object sender, EventArgs e)
        {

        }

        private void MemoryButton_Clicked(object sender, EventArgs e)
        {

        }

        private void ButtonNum_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (Entrada.Text.Equals("0"))
            {
                Entrada.Text = btn.Text;
            }
            else
            {
                if (Entrada.Text.Length < 15)
                {
                    Entrada.Text += btn.Text;
                }
            }
        }

        private void DecimalButton_Clicked(object sender, EventArgs e)
        {

        }

        private void OperationButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}

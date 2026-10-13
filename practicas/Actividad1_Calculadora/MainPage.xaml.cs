using System;

namespace Actividad1_Calculadora
{
    public partial class MainPage : ContentPage
    {
        double left = 0;
        char operation = '0';
        double right = 0;
        double mem = 0;
        public MainPage()
        {
            InitializeComponent();

        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            Entrada.Text = "0";
            left = 0;
            right = 0;
            operation = '0';
            mem = 0;
            Mem.Text = "Mem:" + Convert.ToString(mem);
        }

        private void EqualsButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                right = Convert.ToDouble(Entrada.Text);
                double resultado = operation switch
                {
                    '+' => left + right,
                    '-' => left - right,
                    '*' => left * right,
                    '/' => right == 0 ? throw new DivideByZeroException() : left / right,
                    _ => throw new NotImplementedException(),
                };
                Entrada.Text = Convert.ToString(resultado);
                operation = '0';
                right = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void MemoryButton_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            try
            {
                switch (btn.Text)
                {
                    case "MC":
                        mem = 0;
                        break;
                    case "M+":
                        mem += Convert.ToDouble(Entrada.Text);
                        break;
                    case "M-":
                        mem -= Convert.ToDouble(Entrada.Text);
                        break;
                    case "M":
                        Entrada.Text = Convert.ToString(mem);
                        break;
                }
                Mem.Text = "Mem:" + Convert.ToString(mem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ButtonNum_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (ControlarSiExisteOperacion())
            {
                try
                {
                    left = Convert.ToDouble(Entrada.Text.Substring(0, Entrada.Text.Length - 1));
                    operation = Entrada.Text.Last();
                    Entrada.Text = "0";
                }
                catch (Exception ex) {
                    left = 0;
                    Console.WriteLine(ex.ToString());
                }
            }
            if (Entrada.Text.Equals("0"))
            {
                Entrada.Text = btn.Text;
            }
            else
            {
                if (Entrada.Text.Length < 11)
                {
                    Entrada.Text += btn.Text;
                }
            }
        }

        private void DecimalButton_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            char ultimo = Entrada.Text.Last();
            if (!Entrada.Text.Last().ToString().Contains(",") && !"+-*/".Contains(Entrada.Text.Last().ToString()) && !Entrada.Text.Contains(","))
            {
                Entrada.Text += ",";
            }
            
        }

        private void OperationButton_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            char ultimo = Entrada.Text.Last();
            if ("+-*/".Contains(ultimo))
            {
                Entrada.Text = Entrada.Text.Remove(Entrada.Text.Length - 1) + btn.Text;

            }
            else if (!",".Contains(ultimo))
            {
                Entrada.Text = Entrada.Text + btn.Text;
            }

        }

        private bool ControlarSiExisteOperacion()
        {
            if ("+-*/".Contains(Entrada.Text.Last().ToString()))
            {
                return true;
            }
            return false;
        }
    }
}

namespace trivial;

public partial class Paises : ContentPage
{
    private string[] capitales;
    private string[] paises;
    private Random random = new Random();
    private List<int> indicesDisponibles = new List<int>();
    private int indiceCorrectoActual;
    private int aciertos = 0;
    private int rondaActual = 0;
    private const int TOTAL_PREGUNTAS = 10;
    private bool respondido = false;
    private Button[] botonesOpciones;
    public Paises()
	{
		InitializeComponent();
        this.capitales = new string[] { "Berlín", "Viena", "Bruselas", "Sofía", "Nicosia", "Zagreb", "Copenhague",
                "Bratislava", "Liubliana" , "Madrid", "Tallin", "Helsinki", "París", "Atenas", "Budapest", "Dublín",
                "Roma", "Riga", "Vilna", "Luxemburgo", "La Valeta", "Ámsterdam", "Varsovia", "Lisboa", "Londres",
                "Praga", "Bucarest", "Estocolmo" };
        this.paises = new string[] { "Alemania", "Austria", "Bélgica", "Bulgaria", "Chipre", "Croacia", "Dinamarca",
                "Eslovaquia", "Eslovenia", "España", "Estonia", "Finlandia", "Francia", "Grecia", "Hungría", "Irlanda",
                "Italia", "Letonia", "Lituania", "Luxemburgo", "Malta", "Países Bajos", "Polonia", "Portugal",
                "Reino Unido", "Chéquia", "Rumanía", "Suecia" };
        this.botonesOpciones = new Button[] { opt1, opt2, opt3, opt4 };
        IniciarJuego();
    }
    private void IniciarJuego()
    {
        aciertos = 0;
        rondaActual = 0;

        indicesDisponibles.Clear();
        for (int i = 0; i < capitales.Length; i++)
        {
            indicesDisponibles.Add(i);
        }
        CargarSiguientePregunta();
    }
    private void CargarSiguientePregunta()
    {
        respondido = false;
        rondaActual++;
        int posAleatoria = random.Next(indicesDisponibles.Count);
        indiceCorrectoActual = indicesDisponibles[posAleatoria];
        indicesDisponibles.RemoveAt(posAleatoria);

        lblPregunta.Text = $"Pregunta {rondaActual}/{TOTAL_PREGUNTAS}: ¿Cuál es la capital de {paises[indiceCorrectoActual]}?";

        List<string> opciones = new List<String> { capitales[indiceCorrectoActual] };
        while (opciones.Count < 4)
        {
            string capitalFalse = capitales[random.Next(capitales.Length)];
            if (!opciones.Contains(capitalFalse))
            {
                opciones.Add(capitalFalse);
            }
        }

        opciones = opciones.OrderBy(x => random.Next()).ToList();

        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            botonesOpciones[i].Text = opciones[i];
            botonesOpciones[i].BackgroundColor = Colors.LightGray;
            botonesOpciones[i].IsEnabled = true;
        }
    }
    private void Opcion_Clicked(object sender, EventArgs e)
    {
        if (respondido) return;

        respondido = true;
        Button botonPulsado = (Button)sender;
        string capitalCorrecto = capitales[indiceCorrectoActual];

        if (botonPulsado.Text == capitalCorrecto)
        {
            botonPulsado.BackgroundColor = Colors.LightGreen;
            aciertos++;
        }
        else
        {
            botonPulsado.BackgroundColor = Colors.Salmon;
            foreach (var btn in botonesOpciones)
            {
                if (btn.Text == capitalCorrecto)
                {
                    btn.BackgroundColor = Colors.LightGreen;
                    break;
                }
            }
        }

    }
    private async void next_Clicked(object sender, EventArgs e)
    {
        if (!respondido)
        {
            await DisplayAlert("Atención", "Debes seleccionar una respuesta antes de continuar", "OK");
            return;
        }
        if (rondaActual >= TOTAL_PREGUNTAS)
        {
            await DisplayAlert("Fin de la partida", $"Has acertado {aciertos} de {TOTAL_PREGUNTAS} preguntas.", "Volver a jugar");
            IniciarJuego();
        }
        else
        {
            CargarSiguientePregunta();
        }
    }

    private async void home_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}

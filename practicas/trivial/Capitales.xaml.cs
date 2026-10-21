namespace trivial;

public partial class Capitales : ContentPage
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
	public Capitales()
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

        lblPregunta.Text = $"Pregunta {rondaActual}/{TOTAL_PREGUNTAS}: ¿De qué país es la capital {capitales[indiceCorrectoActual]}?";

        List<string> opciones = new List<String>{paises[indiceCorrectoActual]};
        while (opciones.Count < 4)
        {
            string paisFalso = paises[random.Next(paises.Length)];
            if (!opciones.Contains(paisFalso))
            {
                opciones.Add(paisFalso); 
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
        string paisCorrecto = paises[indiceCorrectoActual];

        if (botonPulsado.Text == paisCorrecto)
        {
            botonPulsado.BackgroundColor = Colors.LightGreen;
            aciertos++;
        }
        else
        {
            botonPulsado.BackgroundColor = Colors.Salmon;
            foreach (var btn in botonesOpciones)
            {
                if (btn.Text == paisCorrecto)
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


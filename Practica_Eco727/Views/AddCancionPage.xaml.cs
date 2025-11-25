using Practica_Eco727.Models;
using Practica_Eco727.Helpers;

namespace Practica_Eco727.Views;

public partial class AddCancionPage : ContentPage
{
    FirebaseHelper firebaseHelper = new FirebaseHelper();

    public AddCancionPage()
    {
        InitializeComponent();
    }

    private async void OnAddCancionClicked(object sender, EventArgs e)
    {
        // Validar que ningún campo esté vacío
        if (string.IsNullOrWhiteSpace(TituloEntry.Text) ||
            string.IsNullOrWhiteSpace(InterpreteEntry.Text) ||
            string.IsNullOrWhiteSpace(GeneroEntry.Text) ||
            string.IsNullOrWhiteSpace(NacionalidadEntry.Text) ||
            string.IsNullOrWhiteSpace(AnioLanzamientoEntry.Text))
        {
            await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
            return;
        }

        // Validar que el año sea un número válido
        if (!int.TryParse(AnioLanzamientoEntry.Text, out int anio))
        {
            await DisplayAlert("Error", "El año debe ser un número válido", "OK");
            return;
        }

        try
        {
            var nuevaCancion = new Cancion
            {
                Titulo = TituloEntry.Text,
                Interprete = InterpreteEntry.Text,
                Genero = GeneroEntry.Text,
                Nacionalidad = NacionalidadEntry.Text,
                AnioLanzamiento = anio
            };

            await firebaseHelper.AddCancion(nuevaCancion);
            await DisplayAlert("Éxito", "Canción agregada correctamente", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Captura cualquier error inesperado
            await DisplayAlert("Error", $"No se pudo agregar la canción: {ex.Message}", "OK");
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}

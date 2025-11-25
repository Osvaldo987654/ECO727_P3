using Practica_Eco727.Models;
using Practica_Eco727.Helpers;

namespace Practica_Eco727.Views;

public partial class EditCancionPage : ContentPage
{
    private FirebaseHelper firebaseHelper = new FirebaseHelper();
    private Cancion cancion;

    public EditCancionPage(Cancion cancion)
    {
        InitializeComponent();
        this.cancion = cancion;

        // Cargamos los datos de la canción
        TituloEntry.Text = cancion.Titulo;
        InterpreteEntry.Text = cancion.Interprete;
        GeneroEntry.Text = cancion.Genero;
        NacionalidadEntry.Text = cancion.Nacionalidad;
        AnioLanzamientoEntry.Text = cancion.AnioLanzamiento.ToString();
    }

    private async void OnUpdateCancionClicked(object sender, EventArgs e)
    {
        // Validar que no haya campos vacíos
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
            // Actualizar los valores de la canción
            cancion.Titulo = TituloEntry.Text;
            cancion.Interprete = InterpreteEntry.Text;
            cancion.Genero = GeneroEntry.Text;
            cancion.Nacionalidad = NacionalidadEntry.Text;
            cancion.AnioLanzamiento = anio;

            await firebaseHelper.UpdateCancion(cancion.Id, cancion);
            await DisplayAlert("Éxito", "Canción actualizada correctamente", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Captura cualquier error inesperado
            await DisplayAlert("Error", $"No se pudo actualizar la canción: {ex.Message}", "OK");
        }
    }

    private void OnBackClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }


}

using Practica_Eco727.Models;
using Practica_Eco727.Helpers;

namespace Practica_Eco727.Views;

public partial class ListCancionPage : ContentPage
{
    FirebaseHelper firebaseHelper = new FirebaseHelper();

    // Lista original completa (para no perder datos al buscar)
    private List<Cancion> cancionesOriginales = new();

    public ListCancionPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadCanciones(); // se recarga cada que entras a la pantalla
    }

    private async void LoadCanciones()
    {
        var canciones = await firebaseHelper.GetAllCanciones();

        // Guardamos una copia completa para filtrar sin perder datos
        cancionesOriginales = canciones.ToList();

        // Mostrar lista completa
        CancionesListView.ItemsSource = cancionesOriginales;
    }

    // 🔍 BÚSQUEDA EN TIEMPO REAL
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string filtro = e.NewTextValue?.ToLower() ?? "";

        var filtradas = cancionesOriginales.Where(c =>
            (c.Titulo != null && c.Titulo.ToLower().Contains(filtro)) ||
            (c.Interprete != null && c.Interprete.ToLower().Contains(filtro)) ||
            (c.Genero != null && c.Genero.ToLower().Contains(filtro))
        ).ToList();

        CancionesListView.ItemsSource = filtradas;
    }

    private async void OnEditCancionClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var cancion = button?.BindingContext as Cancion;

        if (cancion != null)
        {
            await Navigation.PushAsync(new EditCancionPage(cancion));
        }
    }

    private async void OnDeleteCancionClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var cancion = button?.BindingContext as Cancion;

        if (cancion != null && !string.IsNullOrEmpty(cancion.Id))
        {
            bool confirm = await DisplayAlert(
                "Confirmar eliminación",
                $"¿Deseas eliminar la canción '{cancion.Titulo}'?",
                "Sí",
                "No"
            );

            if (!confirm) return;

            await firebaseHelper.DeleteCancion(cancion.Id);
            await DisplayAlert("Éxito", "Canción eliminada", "OK");
            LoadCanciones();
        }
        else
        {
            await DisplayAlert("Error", "No se pudo encontrar la canción", "OK");
        }
    }

    private void OnBackClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}

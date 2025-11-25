using Practica_Eco727.Models;
using Practica_Eco727.Helpers;

namespace Practica_Eco727.Views;

public partial class SearchCancionPage : ContentPage
{
    FirebaseHelper firebaseHelper = new FirebaseHelper();
    private List<Cancion> _canciones = new();

    public SearchCancionPage()
    {
        InitializeComponent();
        LoadCanciones();
    }

    private async void LoadCanciones()
    {
        _canciones = await firebaseHelper.GetAllCanciones();
        ResultsListView.ItemsSource = _canciones;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string term = e.NewTextValue?.Trim().ToLower() ?? "";

        if (string.IsNullOrWhiteSpace(term))
        {
            ResultsListView.ItemsSource = _canciones;
            return;
        }

        var filtradas = _canciones
            .Where(c =>
                (!string.IsNullOrEmpty(c.Titulo) && c.Titulo.ToLower().Contains(term)) ||
                (!string.IsNullOrEmpty(c.Interprete) && c.Interprete.ToLower().Contains(term)) ||
                (!string.IsNullOrEmpty(c.Genero) && c.Genero.ToLower().Contains(term)) ||
                (!string.IsNullOrEmpty(c.Nacionalidad) && c.Nacionalidad.ToLower().Contains(term)) ||
                c.AnioLanzamiento.ToString().Contains(term)
            )
            .ToList();

        ResultsListView.ItemsSource = filtradas;
    }


    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}

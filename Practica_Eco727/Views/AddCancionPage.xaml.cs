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
		var nuevaCancion = new Cancion
		{
			Titulo = TituloEntry.Text,
			Interprete = InterpreteEntry.Text,
			Genero = GeneroEntry.Text,
			Nacionalidad = NacionalidadEntry.Text,
            AnioLanzamiento = int.Parse(AnioLanzamientoEntry.Text)
		};
		await firebaseHelper.AddCancion(nuevaCancion);
		await DisplayAlert("Éxito", "Canción agregada correctamente", "OK");
        await Navigation.PopAsync();
    }

    private void OnBackClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }



}
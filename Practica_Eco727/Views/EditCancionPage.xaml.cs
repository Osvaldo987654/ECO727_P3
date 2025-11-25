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

		//cargamos los datos de la canción

		TituloEntry.Text = cancion.Titulo;
		InterpreteEntry.Text = cancion.Interprete;
		GeneroEntry.Text = cancion.Genero;
		NacionalidadEntry.Text = cancion.Nacionalidad;	
        AnioLanzamientoEntry.Text = cancion.AnioLanzamiento.ToString();
    }


	private async void OnUpdateCancionClicked(object sender, EventArgs e)
	{
		cancion.Titulo = TituloEntry.Text;
		cancion.Interprete = InterpreteEntry.Text;
		cancion.Genero = GeneroEntry.Text;
		cancion.Nacionalidad = NacionalidadEntry.Text;
        cancion.AnioLanzamiento = int.Parse(AnioLanzamientoEntry.Text);
		await firebaseHelper.UpdateCancion(cancion.Id, cancion);
		await DisplayAlert("Éxito", "Canción actualizada correctamente", "OK");
		await Navigation.PopAsync();
    }

}
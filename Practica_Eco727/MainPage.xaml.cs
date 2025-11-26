using Practica_Eco727.Views;

namespace Practica_Eco727;


public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnAddCancionClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddCancionPage());
    }

    private async void OnSearchCancionClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SearchCancionPage());
    }

    private async void OnListCancionClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ListCancionPage());
    }



    private async void OnHelpClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HelpPage());
    }


    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Cerrar Sesión", "¿Deseas cerrar sesión?", "Sí", "No");

        if (confirm)
        {
            // Cerrar sesión (FirebaseAuth, Preferencias, etc.)
        #if ANDROID
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        #elif IOS
            // iOS no permite cerrar la app programáticamente.
            // Aquí podrías limpiar datos, cerrar sesión o navegar a Login.
        #elif WINDOWS
            Application.Current.Quit();
        #endif
        }
    }

}

using System.Windows.Input;

namespace Practica_Eco727.Views;

public partial class HelpPage : ContentPage
{
    public ICommand OpenUrlCommand { get; }

    public HelpPage()
    {
        InitializeComponent();

        OpenUrlCommand = new Command<string>(async (url) =>
        {
            try
            {
                await Launcher.OpenAsync(url);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo abrir: {url}\n{ex.Message}", "OK");
            }
        });

        BindingContext = this;
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}

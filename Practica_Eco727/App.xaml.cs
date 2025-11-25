using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Networking;

namespace Practica_Eco727
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Detecta pérdida de internet en tiempo real
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());

            // Asegura que la página ya está renderizada
            window.Created += async (s, e) =>
            {
                await Task.Delay(300); // Espera breve para evitar null

                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await window.Page.DisplayAlert(
                        "Sin conexión",
                        "La aplicación requiere Internet para funcionar, por lo que se cerrará.",
                        "Aceptar"
                    );

                    CloseApp();
                }
            };

            return window;
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                // Espera a que exista una página válida
                var page = App.Current?.MainPage;

                if (page == null)
                {
                    await Task.Delay(300);
                    page = App.Current?.MainPage;
                }

                if (page != null)
                {
                    await page.DisplayAlert(
                        "Sin conexión",
                        "Se ha perdido la conexión a Internet.\nLa aplicación se cerrará.",
                        "Aceptar"
                    );
                }

                CloseApp();
            }
        }

        private void CloseApp()
        {
#if ANDROID
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            System.Environment.Exit(0);

#elif WINDOWS
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();

#elif IOS
            UIKit.UIApplication.SharedApplication.PerformSelector(
                new ObjCRuntime.Selector("suspend"),
                null,
                0.0f
            );
#endif
        }
    }
}

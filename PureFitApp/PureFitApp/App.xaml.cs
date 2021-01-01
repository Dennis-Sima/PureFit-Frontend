using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PureFitApp
{
    public partial class App : Application
    {
            public App()
            {
                InitializeComponent();
            }

            protected override void OnStart()
            {
                // Damit wir asynchron vom Server die ersten Daten laden können, verwenden wir OnStart
                // und nicht den Konstruktor für die Initialisierung der ersten Seite.
                //MainPage = new MainPage(new DashboardPage(await DashboardViewModel.FactoryAsync()));
                MainPage = new LoginPage();
            }

            protected override void OnSleep()
            {
                // Handle when your app sleeps
            }

            protected override void OnResume()
            {
                // Handle when your app resumes
            }
        }
    }
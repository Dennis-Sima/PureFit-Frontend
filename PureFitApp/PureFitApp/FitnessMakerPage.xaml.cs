using PureFitApp.Service;
using PureFitApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using System.Globalization;
using PureFitApp.Dto;
using System.Net.Http;
using Application = Xamarin.Forms.Application;

namespace PureFitApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FitnessMakerPage : ContentPage
    {
        private TimeSpan span { get; set; }

        public FitnessMakerPage()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    span = span.Subtract(TimeSpan.FromSeconds(1));
                    lbltime.Text = span.ToString(@"hh\:mm\:ss");
                    if (span.Seconds == 0)
                    {
                        finishedAsync();
                    }
                });
                return true;
            });

        }




        public FitnessMakerPage(long currentId) : this()
        {
            // Das Viewmodel mit der anzuzeigenden Klasse initialisieren.
            BindingContext = new FitnessMakerViewModel(currentId);
        }




        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            try
            {
                FitnessMakerViewModel vm = BindingContext as FitnessMakerViewModel;
                await vm?.LoadUebung();

                span = TimeSpan.Parse(vm.FitnessUebung.Dauer);

            }
            // Diese Fehler dürften hier gar nicht mehr auftreten, da das Laden der Seite nur mit
            // gültiger Rolle zu ermöglichen ist.
            catch (ServiceException err) when (err.HttpStatusCode == (int)HttpStatusCode.Unauthorized)
            {
                await App.Current.MainPage.DisplayAlert("Fehler", "Nicht angemeldet", "OK");
            }
            catch (ServiceException err) when (err.HttpStatusCode == (int)HttpStatusCode.Forbidden)
            {
                await App.Current.MainPage.DisplayAlert("Fehler", "Keine Berechtigung", "OK");
            }
        }
        private async void Finished_Clicked(object sender, EventArgs e)
        {
            finishedAsync();
        }

        private async Task finishedAsync()
        {
            //string result = await DisplayPromptAsync("Workout rating","-How do you feel-",  maxLength: 1, keyboard: Keyboard.Numeric);

            try
            {
                FitnessHistoryDto w = new FitnessHistoryDto
                {
                    Date = DateTime.Now+"",
                    Bewertung = "3.67",
                    UebungsNr = (long)Convert.ToDouble(Nr.Text)
                };
                await RestService.Instance.SendAsync<FitnessHistoryDto>(HttpMethod.Post, "fitness/addWorkout", "", w);
                NavigationPage newNavigation = new NavigationPage();
                await newNavigation.PushAsync(new DashboardPage());

                Application.Current.MainPage = new MainPage(newNavigation);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "Did not save changes!", "OK");

            }

        }
    }
}
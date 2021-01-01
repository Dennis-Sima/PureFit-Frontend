
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PureFitApp.Service;
using PureFitApp.Dto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PureFitApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            // Damit der Button nicht mehrfach während des Ladens gedrückt wird, deaktivieren wir ihn.
            Login.IsEnabled = false;
            try
            {
                if (await RestService.Instance.TryLoginAsync(new Dto.UserDto
                {
                Username = this.Username.Text,
                Password = this.Password.Text
                 }))
                {
                    NavigationPage newNavigation = new NavigationPage();
                    await newNavigation.PushAsync(new DashboardPage());

                    Application.Current.MainPage = new MainPage(newNavigation);
                }
                else
                    await DisplayAlert("Warning", "Please check your Username or Password!", "OK");
            }
            catch (Exception err)
            {
                await DisplayAlert("Warning", "Ups, an error has occurred!", "OK");
            }
            // Damit der Button sicher wieder aktiviert wird, kommt der Block ins Finally
            finally
            {
                Login.IsEnabled = true;
            }
        }
        private async void Sign_Clicked(object sender, EventArgs e)
        {        
                Application.Current.MainPage = new RegisterPage();
        }
    }
}
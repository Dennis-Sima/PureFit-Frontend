using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFitApp.Dto;
using PureFitApp.Service;
using PureFitApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PureFitApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new KundeViewModel();
        }
        private async Task<List<string>> GetLevelAsync()
        {
            var a = await RestService.Instance.GetTrainingslevelsAsync();
            return a.Select(s => s.Name).ToList<string>();
        }

        /// <summary>
        /// Lädt die Daten vom Server, wenn die Seite angezeigt wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            KundeViewModel vm = BindingContext as KundeViewModel;
            await vm.LoadTrainingslevel();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
        private async void Register_Clicked(object sender, EventArgs e)
        {
            decimal a;
            if (String.IsNullOrEmpty(Username.Text))
                await DisplayAlert("Warning", "Please fill in your Username!", "OK");
            else if (String.IsNullOrEmpty(Password.Text))
                await DisplayAlert("Warning", "Please fill in your Password!", "OK");
            else if (Password.Text.Length < 8)
                await DisplayAlert("Warning", "Your password musst have a minimum of 8 digits!", "OK");
            else if (String.IsNullOrEmpty(Firstname.Text))
                await DisplayAlert("Warning", "Please fill in your first name!", "OK");
            else if (String.IsNullOrEmpty(Surname.Text))
                await DisplayAlert("Warning", "Please fill in your surname!", "OK");
            else if (Gender.SelectedItem == null)
                await DisplayAlert("Warning", "Please select your gender!", "OK");
            else if (String.IsNullOrEmpty(Height.Text))
                await DisplayAlert("Warning", "Please fill in your height!", "OK");
            else if (!Decimal.TryParse(Height.Text, out a))
                await DisplayAlert("Warning", "Height should be a number -> like 182.60!", "OK");
            else if (String.IsNullOrEmpty(Weight.Text))
                await DisplayAlert("Warning", "Please fill in your weight", "OK");
            else if (!Decimal.TryParse(Weight.Text, out a))
                await DisplayAlert("Warning", "Weight should be a number -> like 70.34!", "OK");
            else if (Gebdate.Date == null)
                await DisplayAlert("Warning", "Please fill in your birthday!", "OK");
            else if (Trainingslevel.SelectedItem == null)
                await DisplayAlert("Warning", "Please fill in your trainingslevel!", "OK");
            else if (check.IsChecked == false)
                await DisplayAlert("Warning", "You have to agree with terms & conditions!", "OK");
            else
            {
                try
                {
                    if (await RestService.Instance.TryRegisterAsync(new Dto.UserKundenDto
                    {

                        Username = this.Username.Text,
                        Password = this.Password.Text,
                        Vorname = this.Firstname.Text,
                        Zuname = this.Surname.Text,
                        Geschlecht = this.Gender.SelectedItem.ToString(),
                        Groesse = this.Height.Text,
                        Gewicht = this.Weight.Text,
                        GebDatum = this.Gebdate.Date.ToString("dd.MM.yyyy"),
                        Email = this.Email.Text,
                        TelefonNr = this.Phone.Text,
                        Trainingslevel = ((TrainingslevelDto)this.Trainingslevel.SelectedItem).Name
                    }))
                    {
                        Application.Current.MainPage = new LoginPage();
                    }
                    else
                        await DisplayAlert("Warning", "Ups, an error has occurred!", "OK");
                }
                catch (Exception e1)
                {
                    await DisplayAlert("Warning", "Ups, an error has occurred!", "OK");
                }


            }
        }
    }
}
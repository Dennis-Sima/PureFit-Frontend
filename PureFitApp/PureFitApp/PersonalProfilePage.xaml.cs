using PureFitApp.Dto;
using PureFitApp.Service;
using PureFitApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PureFitApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalProfilePage : ContentPage
    {
        public PersonalProfilePage()
        {
            InitializeComponent();
            BindingContext = new KundeViewModel();
        }

        /// <summary>
        /// Lädt die Daten vom Server, wenn die Seite angezeigt wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            KundeViewModel vm = BindingContext as KundeViewModel;
            await vm.LoadKundenData();
            await vm.LoadTrainingslevel();

            //A little ugly solution -> but to set the datepickers date to an string in Kundendata, 
            //we must parse it first to date
            Gebdate.Date = DateTime.Parse(vm.KundenData.GebDatum);
        }


       
        private async void Save(object sender, EventArgs e)
        {
            bool answer  = await DisplayAlert("Update?", "Are you sure, you want to change your data?", "Yes", "No");
            if (answer)
            {

                try
                {
                    KundenDto k = new KundenDto
                    {
                        Vorname = Firstname.Text,
                        Zuname = this.Surname.Text,
                        Geschlecht = this.Gender.SelectedItem.ToString(),
                        Groesse = this.Height.Text,
                        Gewicht = this.Weight.Text,
                        GebDatum = this.Gebdate.Date.ToString("dd.MM.yyyy"),
                        Trainingslevel = ((TrainingslevelDto)this.Trainingslevel.SelectedItem).Name
                    };
                    await RestService.Instance.SendAsync<KundenDto>(HttpMethod.Put, "kunden/editPersonalData", "", k);
                    await DisplayAlert("Warning", "Changes has been saved!", "OK");
                }
                catch(Exception ex)
                {
                    await DisplayAlert("Warning", "Did not save changes!", "OK");

                }
            }
            else
                await DisplayAlert("Warning", "Did not save changes!", "OK");
        }
    }
}
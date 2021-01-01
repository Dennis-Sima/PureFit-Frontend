using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFitApp.Service;
using PureFitApp.ViewModels;
using PureFitApp.Dto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

namespace PureFitApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
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
        }

        /// <summary>
        /// Legt die Detailseite auf den Navigation Stack und lädt diese.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ProfileList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            KundeViewModel vm = BindingContext as KundeViewModel;
            NavigationItem item = e.SelectedItem as NavigationItem;
            if (item.Title == "delete Account")
                 await Navigation.PushAsync(new DeleteAccountPage());
            else
                 await Navigation.PushAsync(new PersonalProfilePage());
        }

    }
}
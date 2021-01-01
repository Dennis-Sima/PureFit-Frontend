using PureFitApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PureFitApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FitnessPage : ContentPage
    {
        public FitnessPage()
        {
            InitializeComponent();
            BindingContext = new WorkoutViewModel();
        }
        /// <summary>
        /// Lädt die Daten vom Server, wenn die Seite angezeigt wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            WorkoutViewModel vm = BindingContext as WorkoutViewModel;
            await vm.LoadFitnessUebungen();
        }

        /// <summary>
        /// Legt die Detailseite auf den Navigation Stack und lädt diese.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void WorkoutList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            WorkoutViewModel vm = BindingContext as WorkoutViewModel;
            //await Navigation.PushAsync(new FitnessMakerPage(vm.SelectedUebung));
            await Navigation.PushAsync(new FitnessMakerPage(vm.SelectedUebung.UebungsNr));

        }
    }
}
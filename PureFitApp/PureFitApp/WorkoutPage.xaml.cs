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
    public partial class WorkoutPage : ContentPage
    {
        /// <summary>
        /// Defaultkonstruktor. Ist private, denn die Seite benötigt ein Initialisiertes ViewModel.
        /// </summary>
        public WorkoutPage()
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
            await vm.LoadFitnessHistory();
        }
    }
}
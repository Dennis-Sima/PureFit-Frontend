using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PureFitApp.Service;
using PureFitApp.Dto;
using Xamarin.Forms;

namespace PureFitApp.ViewModels
{
    /// <summary>
    /// Viewmodel für die Übersichtsseite der Tests
    /// </summary>
    public class WorkoutViewModel : BaseViewModel
    {

        public IEnumerable<FitnessHistoryDto> FitnessHistory { get; set; }
        public IEnumerable<FItnessuebungDto> FitnessUebungen { get; set; }
        public FItnessuebungDto SelectedUebung { get; set; }

        public async Task LoadFitnessHistory()
        {
            try
            {
                SetProperty(nameof(FitnessHistory), await RestService.Instance.GetFitnessHistoryAsync());
            }
            catch (Exception e)
            {
#if DEBUG
                await App.Current.MainPage.DisplayAlert("Fehler", e.ToString(), "OK");
#endif
            }
        }


        public async Task LoadFitnessUebungen()
        {
            try
            {
                SetProperty(nameof(FitnessUebungen), await RestService.Instance.GetFitnessUebungenAsync());
            }
            catch (Exception e)
            {
#if DEBUG
                await App.Current.MainPage.DisplayAlert("Fehler", e.ToString(), "OK");
#endif
            }
        }
    }
}
    


    
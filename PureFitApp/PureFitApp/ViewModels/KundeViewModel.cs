using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PureFitApp.Service;
using PureFitApp.Dto;
using Xamarin.Forms;

namespace PureFitApp.ViewModels
{
    public class KundeViewModel : BaseViewModel
    {

        public UserKundenDto KundenData { get; set; }
        public IEnumerable<TrainingslevelDto> Trainingslevels { get; set; }



        public async Task LoadKundenData()
        {
            try
            {
                SetProperty(nameof(KundenData), await RestService.Instance.GetKundenData());
            }
            catch (Exception e)
            {
#if DEBUG
                await App.Current.MainPage.DisplayAlert("Fehler", e.ToString(), "OK");
#endif
            }
        }
        public async Task LoadTrainingslevel()
        {
            try
            {
                SetProperty(nameof(Trainingslevels), await RestService.Instance.GetTrainingslevelsAsync());
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

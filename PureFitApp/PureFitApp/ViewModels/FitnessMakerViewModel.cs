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
    public class FitnessMakerViewModel : BaseViewModel
    {
        public long CurrentId { get; }

        public FItnessuebungDto FitnessUebung { get; set; }
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="currentId">Die ID der Klasse, dessen Details angezeigt werden.</param>
        public FitnessMakerViewModel(long currentId)
        {
            CurrentId = currentId;
        }
        public async Task LoadUebung()
        {
            try
            {
                SetProperty(nameof(FitnessUebung), await RestService.Instance.GetUebung(CurrentId));
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

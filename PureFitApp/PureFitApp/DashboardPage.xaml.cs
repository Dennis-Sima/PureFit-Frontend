using PureFitApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PureFitApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();

            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")

                page.BackgroundImageSource = ImageSource.FromResource("PureFitApp.Resources.DashboardBild-Mobile.jpg", typeof(ImageResourceExtension).GetTypeInfo().Assembly);
            else
                page.BackgroundImageSource = ImageSource.FromResource("PureFitApp.Resources.DashboardBild.jpg", typeof(ImageResourceExtension).GetTypeInfo().Assembly);
       
        }
        private async void startWorkout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FitnessPage());
        }
    }
}

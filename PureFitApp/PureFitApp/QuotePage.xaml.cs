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
    public partial class QuotePage : ContentPage
    {
        public QuotePage()
        {
            InitializeComponent();
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")

                page.BackgroundImageSource = ImageSource.FromResource("PureFitApp.Resources.QuotesBild-Mobile.jpeg", typeof(ImageResourceExtension).GetTypeInfo().Assembly);
            else
                page.BackgroundImageSource = ImageSource.FromResource("PureFitApp.Resources.QuotesBild.jpeg", typeof(ImageResourceExtension).GetTypeInfo().Assembly);

        }
    }
}
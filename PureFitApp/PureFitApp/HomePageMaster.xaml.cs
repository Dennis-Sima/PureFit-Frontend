using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PureFitApp.Service;
using PureFitApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PureFitApp
{
    public class NavigationItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageMaster : ContentPage
    {
        public ListView ListView;

        public HomePageMaster()
        {
            InitializeComponent();
        }

        private async void NavigationList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MasterDetailPage mainPage = App.Current.MainPage as MasterDetailPage;
            NavigationItem item = e.SelectedItem as NavigationItem;
            if (mainPage != null && item != null)
            {
                if (item.Title == "Log out")
                {
                    RestService.Instance.Logout();
                    Application.Current.MainPage = new LoginPage();
                }
                else if (item.TargetType == typeof(DashboardPage))
                {
                    var page = new DashboardPage();
                    mainPage.Detail = new NavigationPage(page);
                }
                else
                {
                    // Wir brauchen eine Navigation, sonst kommen wir in Android nicht mehr zurück wenn
                    // die Masterpage versteckt wird.
                    mainPage.Detail = new NavigationPage(
                        (Page)Activator.CreateInstance(item.TargetType));
                }
                // Die Masterpage soll nach dem Auswählen verschwinden.
                mainPage.IsPresented = false;
            }
        }
        

            
    }
}
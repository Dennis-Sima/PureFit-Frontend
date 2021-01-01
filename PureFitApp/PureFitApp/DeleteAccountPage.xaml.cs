using PureFitApp.Dto;
using PureFitApp.Service;
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
    public partial class DeleteAccountPage : ContentPage
    {
        public DeleteAccountPage()
        {
            InitializeComponent();
        }
        private async void Save(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Warning!", "Are you sure you want to completely delete your data at any time? (This cannot be undone!)", "Yes", "No");
            if(answer)
            {
                
               
                    if (await RestService.Instance.TryLoginAsync(new Dto.UserDto
                    {
                        Username = this.Username.Text,
                        Password = this.Password.Text
                    }))
                    {
                        await RestService.Instance.SendAsync<KundenDto>(HttpMethod.Delete, "kunden/deleteKundenUser", "", null);
                        await DisplayAlert("Account will be deleted!", "User is logged out", "OK");
                        RestService.Instance.Logout();
                        Application.Current.MainPage = new LoginPage();
                    }
                    else
                        await DisplayAlert("Warning", "Account not deleted!", "OK");

               
            }
            else
              await DisplayAlert("Warning", "Account not deleted!", "OK");
        }
    }
}
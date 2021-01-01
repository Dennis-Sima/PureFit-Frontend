using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using  PureFitApp.Service;
using PureFitApp.ViewModels;

namespace PureFitApp
{
        [DesignTimeVisible(false)]
        public partial class MainPage : MasterDetailPage
    {
            public MainPage()
            {
                InitializeComponent();
            }

        /// <summary>
        /// Initialisiert die Mainpage. Wird in App.OnStart() aufgerufen.
        /// </summary>
        /// <param name="detailPage">Die erste anzuzeigende Seite.</param>
        public MainPage(Page detailPage) : this()
        {
            Detail = detailPage;
        }
    }
    }
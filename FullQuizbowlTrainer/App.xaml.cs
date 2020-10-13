using System;
using FullQuizbowlTrainer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FullQuizbowlTrainer
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzMzOTg0QDMxMzgyZTMzMmUzMFJzenpxMVJ2dzkycHBtM1BtdHZVTHNmeTRYd0c3dXlIZnZVN1pnN1ZHYzQ9");

            InitializeComponent();

            var id = Xamarin.Essentials.Preferences.Get("userid", 243);
            var conn = Xamarin.Essentials.Connectivity.NetworkAccess;
            
            if(id == 243)
            {
                if (conn == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    MainPage = new NavigationPage(new MainPage());
                }
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }            

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#353535");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;

        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

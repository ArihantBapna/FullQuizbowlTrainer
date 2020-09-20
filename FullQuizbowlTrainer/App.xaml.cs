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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjk2OTk1QDMxMzgyZTMyMmUzMFdpM1NVWE0xNUdqUndaelI1T3F4cUFsREJubEZQbW9WS2hGUGlrRStRYkU9");

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
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

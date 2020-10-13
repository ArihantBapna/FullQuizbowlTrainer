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

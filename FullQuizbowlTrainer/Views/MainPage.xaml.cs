using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.Services.Database;
using FullQuizbowlTrainer.Services.Web;
using FullQuizbowlTrainer.ViewModels;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Views
{
    public partial class MainPage : ContentPage
    {
        List<Categories> CategoryDat = new List<Categories>();
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SelectPreferences(CategoryDat));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            DatabaseManager dbM = new DatabaseManager();
            var categoryDat = await dbM.GetCategoryData();
            if (categoryDat != null)
            {
                CategoryDat = categoryDat;
            }
            
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            DatabaseManager dbM = new DatabaseManager();
            List<Answered> answerList = await dbM.GetAllAnswereds();
            await Navigation.PushAsync(new StatsPage(answerList));
        }

        void Logout_Clicked(System.Object sender, System.EventArgs e)
        {
            Xamarin.Essentials.Preferences.Set("userid", 243);
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }

}

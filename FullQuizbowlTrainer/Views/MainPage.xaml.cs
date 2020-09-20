using System;
using System.Collections.Generic;
using System.Diagnostics;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.Services.Database;
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
            await Navigation.PushAsync(new SetCategories(CategoryDat));
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
    }

}

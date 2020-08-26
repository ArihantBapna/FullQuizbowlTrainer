using System;
using System.Collections.Generic;
using System.Diagnostics;
using FullQuizbowlTrainer.Services.Database;
using FullQuizbowlTrainer.ViewModels;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new QuizzingPage());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            DatabaseManager dbM = new DatabaseManager();
            var answerList = await dbM.GetAllAnswers();
            if (answerList != null)
            {
                QuizzingPageViewModel.AnswerList = answerList;
            }
            
        }
    }

}

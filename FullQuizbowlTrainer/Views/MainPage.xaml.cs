using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Stopwatch timer = Stopwatch.StartNew();
            RestService restService = new RestService();
            string wake = await restService.Get("/wake");
            timer.Stop();
            TimeSpan timeSpan = timer.Elapsed;
            Console.WriteLine("Rest results:-" +wake);
            Console.WriteLine("Time took: " + timeSpan.Seconds +"s " +timeSpan.Milliseconds +"ms ");

            timer.Reset();
            AnsweredRest ans = new AnsweredRest();
            ans.answerid = 543;
            ans.buzzed = "buzz2";
            ans.clue = "Someclue";
            ans.questionid = 35481;
            ans.rating = 873.6;
            ans.score = 612.6;
            ans.userid = 683;

            timer.Start();

            string post = await restService.Post("/postData", ans);

            timer.Stop();

            timeSpan = timer.Elapsed;

            Console.WriteLine("Post: " + post);
            Console.WriteLine("Time took: " + timeSpan.Seconds + "s " + timeSpan.Milliseconds + "ms ");

            
            

        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            DatabaseManager dbM = new DatabaseManager();
            List<Answered> answerList = await dbM.GetAllAnswereds();
            await Navigation.PushAsync(new StatsPage(answerList));
        }
    }

}

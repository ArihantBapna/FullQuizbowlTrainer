using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.Services.Web;
using FullQuizbowlTrainer.ViewModels;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Views
{
    public partial class QuizzingPage : ContentPage
    {
        public QuizzingPage(UserProfile userProfile)
        {
            InitializeComponent();
            this.BindingContext = new QuizzingPageViewModel(userProfile);
            
        }

        void Buzz_Clicked(System.Object sender, System.EventArgs e)
        {
            QuizzingPageViewModel qVm = (QuizzingPageViewModel)this.BindingContext;
            if (qVm.isStarted)
            {
                qVm.isStarted = false;
                qVm.IsReading = true;
                qVm.Read();
            }
            else
            {
                if (qVm.IsReading)
                {
                    qVm.IsReading = false;
                    AnswerEntry.Focus();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(qVm.AnsweredText))
                    {
                        qVm.CheckAnswer();
                        MainButton.IsEnabled = false;
                    }
                    else
                    {
                        qVm.IsReading = true;
                        AnswerEntry.Unfocus();
                        qVm.Read();
                    }
                }
            }
        }

        void Next_Clicked(System.Object sender, System.EventArgs e)
        {
            QuizzingPageViewModel qVm = (QuizzingPageViewModel)this.BindingContext;
            qVm.NextQuestion();
            MainButton.IsEnabled = true;
        }

        async void Report_Clicked(System.Object sender, System.EventArgs e)
        {
            QuizzingPageViewModel qVm = (QuizzingPageViewModel)this.BindingContext;

            string category = await DisplayActionSheet("What kind of problem is it: ", null, "Never mind", "Question", "Answer", "Alternate", "Other");
            if(!category.Equals("Never mind"))
            {
                Report report = new Report();
                report.userid = Xamarin.Essentials.Preferences.Get("userid", 243);
                report.questionid = qVm.Question.ID;

                if (category.Equals("Question")) report.category = 0;
                else if (category.Equals("Answer")) report.category = 1;
                else if (category.Equals("Alternate")) report.category = 2;
                else report.category = 3;

                string problem = await DisplayPromptAsync("Problem: ", "Describe the problem in a few words");
                report.problem = problem;

                RestService restService = new RestService();
                var conn = Xamarin.Essentials.Connectivity.NetworkAccess;
                bool result = false;
                if (conn == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    result = await restService.Report(report);
                }
                if (!result)
                {
                    await DisplayAlert("Error", "There was an error sending your report, please check your connection and try again later", "Okay");
                }
                else
                {
                    await DisplayAlert("Success", "Your report has been logged, we will look into it as soon as possible", "Okay");
                }
            }
            

        }
    }
}

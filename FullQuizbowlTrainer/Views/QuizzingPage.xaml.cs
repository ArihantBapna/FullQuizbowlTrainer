using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.ViewModels;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Views
{
    public partial class QuizzingPage : ContentPage
    {
        public QuizzingPage(UserProfile userProfile)
        {
            InitializeComponent();
            this.BindingContext = new QuizzingPageViewModel(userProfile);;
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
                        if (qVm.CheckAnswer()) qVm.AnsweredText = "Correct";
                        else qVm.AnsweredText = "Incorrect";
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
        }
    }
}

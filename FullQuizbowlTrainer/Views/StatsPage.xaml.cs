using System;
using System.Collections.Generic;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.ViewModels;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Views
{
    public partial class StatsPage : ContentPage
    {
        public StatsPage(List<Answered> answerList)
        {
            InitializeComponent();
            this.BindingContext = new StatsPageViewModel(answerList);
        }
    }
}

using System;
using System.Collections.Generic;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.ViewModels;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Views
{
    public partial class QuizzingPage : ContentPage
    {
        public QuizzingPage(List<Answers> answers)
        {
            InitializeComponent();
            this.BindingContext = new QuizzingPageViewModel(answers);;
        }
    }
}

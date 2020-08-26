using System;
using System.Collections.Generic;
using FullQuizbowlTrainer.ViewModels;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Views
{
    public partial class QuizzingPage : ContentPage
    {
        public QuizzingPage()
        {
            InitializeComponent();
            this.BindingContext = new QuizzingPageViewModel();
        }
    }
}

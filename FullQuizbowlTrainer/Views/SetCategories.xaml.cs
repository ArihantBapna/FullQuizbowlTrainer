using System;
using System.Collections.Generic;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.ViewModels;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Views
{
    public partial class SetCategories : ContentPage
    {
        public SetCategories(List<Categories> CategoryDat)
        {
            InitializeComponent();
            this.BindingContext = new SetCategoriesViewModel(CategoryDat);
        }

        void Button_Clicked(object sender, EventArgs args)
        {
            SetCategoriesViewModel.ClearContents((SetCategoriesViewModel)this.BindingContext);
        }
    }
}

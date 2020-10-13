using System;
using System.Collections.Generic;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.ViewModels;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Views
{
    public partial class SetCategories : ContentPage
    {
        INavigation Nav;
        int action;

        public SetCategories(PreferenceId pref, List<Categories> categoryDat, INavigation nav,int act)
        {
            InitializeComponent();
            Nav = nav;
            action = act;
            this.BindingContext = new SetCategoriesViewModel(pref,categoryDat,action);
        }

        void Button_Clicked(object sender, EventArgs args)
        {
            SetCategoriesViewModel.ClearContents((SetCategoriesViewModel)this.BindingContext);
        }

        void Save_Clicked(object sender, EventArgs args)
        {
            if (action == 0) SetCategoriesViewModel.SaveNewPreference((SetCategoriesViewModel)this.BindingContext, Nav, this);
            else if (action == 1) SetCategoriesViewModel.SaveEditPreference((SetCategoriesViewModel)this.BindingContext, Nav);
        }

        protected override bool OnBackButtonPressed()
        {
            // Begin an asyncronous task on the UI thread because we intend to ask the users permission.
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("Leave without saving", "Are you sure you want to exit this page? Unsaved data will not be available.", "Yes", "No"))
                {
                    base.OnBackButtonPressed();

                    await Nav.PopModalAsync();
                }
            });

            // Always return true because this method is not asynchronous.
            // We must handle the action ourselves: see above.
            return true;
        }
    }
}

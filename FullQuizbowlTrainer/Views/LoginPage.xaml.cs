using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FullQuizbowlTrainer.ViewModels;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginPageViewModel();
            
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            LoginButton.IsEnabled = false;
            LoginPageViewModel lVm = (LoginPageViewModel)this.BindingContext;
            int log = await lVm.DoLogin();
            if(log == 0)
            {
                Xamarin.Essentials.Preferences.Set("userid", lVm.Id);
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }else if(log == 1)
            {
                PwdInput.Hint = "Incorrect Password";
            }
            else
            {
                UserInput.Hint = "User not found";
            }

            await Task.Delay(500);
            LoginButton.IsEnabled = true;
            
        }
    }
}

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.Services.Web;

namespace FullQuizbowlTrainer.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Login log;
        public Login Log
        {
            get { return log; }
            set
            {
                log = value;
                OnPropertyChanged("Log");
            }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        private string pwd;
        public string Pwd
        {
            get { return pwd; }
            set
            {
                pwd = value;
                OnPropertyChanged("Pwd");
            }
        }

        public LoginPageViewModel()
        {

        }

        public async Task<int> DoLogin()
        {
            Login l = new Login();
            l.userid = Id;
            l.password = Pwd;
            RestService r = new RestService();
            await r.Get("/wake");
            return await r.GetLogin("/getlogin", l);
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

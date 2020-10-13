using System;
using System.ComponentModel;
using FullQuizbowlTrainer.Models;

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

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

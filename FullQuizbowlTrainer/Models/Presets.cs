using System;
using System.ComponentModel;

namespace FullQuizbowlTrainer.Models
{
    public class Presets : INotifyPropertyChanged
    {
        private string name;
        private double percent;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");

            }
        }

        public double Percent
        {
            get { return percent; }
            set
            {
                percent = value;
                OnPropertyChanged("Percent");
                Xamarin.Forms.MessagingCenter.Send(this, "UpdateTotal");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

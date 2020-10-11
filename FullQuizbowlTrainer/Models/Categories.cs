using System;
using System.ComponentModel;
using FullQuizbowlTrainer.ViewModels;
using SQLite;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Models
{
    public class Categories : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private double user;
        private double percent;
        private double max;

        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public double User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
        public double Percent
        {
            get { return percent; }
            set
            {
                percent = value;
                OnPropertyChanged("Percent");
            }
        }

        public double Max
        {
            get { return max; }
            set
            {
                max = value;
                OnPropertyChanged("Max");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

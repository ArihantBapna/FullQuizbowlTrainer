using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FullQuizbowlTrainer.Models;

namespace FullQuizbowlTrainer.ViewModels
{
    public class StatsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Answered> answeredList;
        public ObservableCollection<Answered> AnsweredList
        {
            get { return answeredList; }
            set
            {
                answeredList = value;
                OnPropertyChanged("AnsweredList");
            }
        }

        public StatsPageViewModel(List<Answered> answerList)
        {
            AnsweredList = new ObservableCollection<Answered>(answerList);
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

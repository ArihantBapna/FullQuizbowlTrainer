using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.Services.Database;

namespace FullQuizbowlTrainer.ViewModels
{
    public class QuizzingPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Answers> listAnswers = new ObservableCollection<Answers>();
        public ObservableCollection<Answers> ListAnswers
        {
            get { return listAnswers; }
            set
            {
                listAnswers = value;
                OnPropertyChanged("ListAnswers");
            }
        }

        private ObservableCollection<Questions> listQuestions = new ObservableCollection<Questions>();
        public ObservableCollection<Questions> ListQuestions
        {
            get { return listQuestions; }
            set
            {
                listQuestions = value;
                OnPropertyChanged("ListQuestions");
            }
        }

        public QuizzingPageViewModel(List<Answers> ans)
        {
            ListAnswers = new ObservableCollection<Answers>(ans);
            SetQuestionsList();
            
        }

        async void SetQuestionsList()
        {
            DatabaseManager db = new DatabaseManager();
            ListQuestions = new ObservableCollection<Questions>(await db.GetQuestionsFromAnswerId(ListAnswers[0].ID));
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

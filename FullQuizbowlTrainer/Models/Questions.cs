using System;
using System.ComponentModel;
using SQLite;

namespace FullQuizbowlTrainer.Models
{
    public class Questions : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private string alternate;
        public string Alternate
        {
            get { return alternate; }
            set
            {
                alternate = value;
                OnPropertyChanged("Alternate");
            }
        }

        private string answer;
        public string Answer
        {
            get { return answer; }
            set
            {
                answer = value;
                OnPropertyChanged("Answer");
            }
        }

        private int answered;
        public int Answered
        {
            get { return answered; }
            set
            {
                answered = value;
                OnPropertyChanged("Answered");
            }
        }

        private int answerID;
        public int AnswerID
        {
            get { return answerID; }
            set
            {
                answerID = value;
                OnPropertyChanged("AnswerID");
            }
        }

        private int category;
        public int Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged("Category");
            }
        }

        private int difficulty;
        public int Difficulty
        {
            get { return difficulty; }
            set
            {
                difficulty = value;
                OnPropertyChanged("Difficulty");
            }
        }

        private int iD;
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID
        {
            get { return iD; }
            set
            {
                iD = value;
                OnPropertyChanged("ID");
            }
        }

        private string prompt;
        public string Prompt
        {
            get { return prompt; }
            set
            {
                prompt = value;
                OnPropertyChanged("Prompt");
            }
        }

        private string question;
        public string Question
        {
            get { return question; }
            set
            {
                question = value;
                OnPropertyChanged("Question");
            }
        }

        private int subcategory;
        public int Subcategory
        {
            get { return subcategory; }
            set
            {
                subcategory = value;
                OnPropertyChanged("Subcategory");
            }
        }

        private string tournament;
        public string Tournament
        {
            get { return tournament; }
            set
            {
                tournament = value;
                OnPropertyChanged("Tournament");
            }
        }

        private int tournamentID;
        public int TournamentID
        {
            get { return tournamentID; }
            set
            {
                tournamentID = value;
                OnPropertyChanged("TournamentID");
            }
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

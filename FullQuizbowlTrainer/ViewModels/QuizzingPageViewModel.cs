using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.Services.Database;
using FullQuizbowlTrainer.Services.Reading;
using FullQuizbowlTrainer.Services.Selector;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.ViewModels
{
    public class QuizzingPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Questions question;
        public Questions Question
        {
            get { return question; }
            set
            {
                question = value;
                OnPropertyChanged("Question");
            }
        }

        private Answers answer;
        public Answers Answer
        {
            get { return answer; }
            set
            {
                answer = value;
                OnPropertyChanged("Answer");
            }
        }

        private string questionText;
        public string QuestionText
        {
            get { return questionText; }
            set
            {
                questionText = value;
                OnPropertyChanged("QuestionText");
            }
        }

        private string buttonState;
        public string ButtonState
        {
            get { return buttonState; }
            set
            {
                buttonState = value;
                OnPropertyChanged("ButtonState");
            }
        }

        private bool isReading;
        public bool IsReading
        {
            get { return isReading; }
            set
            {
                isReading = value;
                OnPropertyChanged("IsReading");
                if (IsReading) ButtonState = "Buzz";
                else
                {
                    if (!isStarted) ButtonState = "Withdraw";
                }
            }
        }

        public ReadQuestions Reader { get; set; }

        public bool isStarted { get; set; }
        

        public QuizzingPageViewModel(UserProfile userProfile)
        {
            NewAnswerLine answerLine = new NewAnswerLine(userProfile);
            Answer = answerLine.SelectedAnswer;
            Task.Run(() => answerLine.GetNewQuestion(Answer)).Wait();
            Question = answerLine.SelectedQuestion;
            QuestionText = "This is where the question will start reading";

            isStarted = true;
            ButtonState = "Start Reading";

            Reader = new ReadQuestions(Question);
            
        }

        public void Read()
        {
            Reader.ReadAQuestion(this);
        }


        public virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

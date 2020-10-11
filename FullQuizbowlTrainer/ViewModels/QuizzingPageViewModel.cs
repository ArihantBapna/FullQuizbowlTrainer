using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using F23.StringSimilarity;
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
                    if (string.IsNullOrEmpty(AnsweredText)) ButtonState = "Withdraw";
                    else ButtonState = "Submit";
                }
            }
        }

        private string answeredText;
        public string AnsweredText
        {
            get { return answeredText; }
            set
            {
                answeredText = value;
                OnPropertyChanged("AnsweredText");
                if (string.IsNullOrEmpty(AnsweredText))
                {
                    ButtonState = "Withdraw";
                }
                else
                {
                    ButtonState = "Submit";
                }
            }
        }

        private string finalAnswer;
        public string FinalAnswer
        {
            get { return finalAnswer; }
            set
            {
                finalAnswer = value;
                OnPropertyChanged("FinalAnswer");
            }
        }

        private string prompts;
        public string Prompts
        {
            get { return prompts; }
            set
            {
                prompts = value;
                OnPropertyChanged("Prompts");
            }
        }

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

        private bool isCompleted;
        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                isCompleted = value;
                OnPropertyChanged("IsCompleted");
            }
        }

        public ReadQuestions Reader { get; set; }

        public bool isStarted { get; set; }

        public UserProfile UserProfile { get; set; }
        
        public QuizzingPageViewModel(UserProfile userProfile)
        {
            UserProfile = userProfile;
            NextQuestion();
            QuestionText = "This is where the question will start reading";
            
        }

        public void Read()
        {
            Reader.ReadAQuestion(this);
        }

        public bool CheckAnswer()
        {
            var l = new NormalizedLevenshtein();
            QuestionText = Question.Question;
            FinalAnswer = Question.Answer;
            Alternate = Question.Alternate;
            Prompts = Question.Prompt;
            IsCompleted = true;
            Question.Answered++;
            if (l.Distance(AnsweredText,Question.Answer) <= 0.8)
            {
                UpdateScoreAndRating(true);
                return true;
            }
            else
            {
                UpdateScoreAndRating(false);
                return false;
            }
        }

        public async void UpdateScoreAndRating(bool corr)
        {
            
            int actual = corr ? 1 : 0;
            var ans = UserProfile.Answers.FirstOrDefault(x => x.ID == Answer.ID);
            var cat = UserProfile.Categories.FirstOrDefault(x => x.Id == Answer.Category);
            double k = (0.8 - (Reader.sentenceAt / Reader.sentences.Length)) * 30;
            Console.WriteLine("k value is : " + k);
            double prob = ProbabilityofCorr(ans.Rating, cat.User);
            cat.User += k * (actual - prob);
            ans.Rating += k * ((1 - actual) - (1 - prob));
            Console.WriteLine("Actual: " + actual);

            if (corr)
            {
                ans.Corrects++;
                
            }
            else
            {
                ans.Negs++;
            }
            int d = ans.Difficulty * 10;
            Console.WriteLine("Initial score: " + ans.Score);
            ans.Score -= d*((ans.Corrects - ans.Negs) * Math.Log10(Math.Pow(d,2) + 1));
            Console.WriteLine("Final Score: " + ans.Score);
            Console.WriteLine("New user category score: " + cat.User);
            Console.WriteLine("New answerline rating: " + ans.Rating);
            Console.WriteLine("New answerline score: " + ans.Score);
            Console.WriteLine("Trying to update database");
            DatabaseManager dbM = new DatabaseManager();
            await dbM.UpdateAnswer(ans);
            await dbM.UpdateQuestion(Question);
            await dbM.UpdateUserCategory(cat);

        }

        public double ProbabilityofCorr(double answerRating, double userRating)
        {
            double probability;
            Console.WriteLine("Answer rating: " + answerRating);
            Console.WriteLine("User rating: " + userRating);
            probability = 1 / (1 + Math.Pow(10, (answerRating - userRating)/400));
            Console.WriteLine("probability: " + probability);

            return probability;
        }



        public void NextQuestion()
        {
            NewAnswerLine answerLine = new NewAnswerLine(UserProfile);
            Answer = answerLine.SelectedAnswer;
            Task.Run(() => answerLine.GetNewQuestion(Answer)).Wait();
            Question = answerLine.SelectedQuestion;
            AnsweredText = "";
            isStarted = true;
            ButtonState = "Start Reading";
            Prompts = "";
            Alternate = "";
            FinalAnswer = "";
            Reader = new ReadQuestions(Question, this);
        }


        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

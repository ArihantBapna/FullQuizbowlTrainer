using System;
using System.Threading.Tasks;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.ViewModels;

namespace FullQuizbowlTrainer.Services.Reading
{
    public class ReadQuestions
    {
        public String[] WordsOfQuestions { get; set; }
        int positionAt;

        public ReadQuestions(Questions selectedQuestion)
        {
            WordsOfQuestions = selectedQuestion.Question.Split(' ');
            positionAt = 0;
        }

        public async void ReadAQuestion(QuizzingPageViewModel qVm)
        {
            for(int i = positionAt; i < WordsOfQuestions.Length; i++)
            {
                await Task.Delay(300);
                qVm.QuestionText += WordsOfQuestions[i] +" ";
                positionAt += 1;
                if (!qVm.IsReading)
                {
                    break;
                }
                
            }
        }
    }
}

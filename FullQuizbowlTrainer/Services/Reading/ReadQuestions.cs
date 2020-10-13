using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.ViewModels;

namespace FullQuizbowlTrainer.Services.Reading
{
    public class ReadQuestions
    {
        public string[] WordsOfQuestions { get; set; }
        public string[] sentences { get; set; }

        public int positionAt;
        public int sentenceAt;

        public ReadQuestions(Questions selectedQuestion, QuizzingPageViewModel qVm)
        {
            sentences = Regex.Split(selectedQuestion.Question, @"(?<=[\.!\?])\s+");
            positionAt = 0;
            sentenceAt = 0;
            qVm.QuestionText = "";
        }

        public async void ReadAQuestion(QuizzingPageViewModel qVm)
        {
            WordsOfQuestions = sentences[sentenceAt].Split(' ');

            qVm.CurrentClue = sentences[sentenceAt];

            for(int i = positionAt; i < WordsOfQuestions.Length; i++)
            {
                await Task.Delay(WordsOfQuestions[i].Length * 70);
                qVm.QuestionText += WordsOfQuestions[i] +" ";
                positionAt += 1;
                if (!qVm.IsReading)
                {
                    break;
                }
                
            }

            if (qVm.IsReading)
            {
                if (sentenceAt != sentences.Length - 1)
                {
                    sentenceAt += 1;
                    positionAt = 0;

                    if (sentenceAt / sentences.Length <= 0.3) qVm.k = 50;
                    else if (sentenceAt / sentences.Length <= 0.6) qVm.k = 30;
                    else qVm.k = 15;

                    await Task.Delay(1000);
                    qVm.QuestionText = "";
                    ReadAQuestion(qVm);
                }
                else
                {
                    await Task.Delay(5000);
                    if (!qVm.IsCompleted)
                    {
                        qVm.IsCompleted = true;
                        qVm.NextQuestion();
                    }
                    
                }
            }

        }
    }
}

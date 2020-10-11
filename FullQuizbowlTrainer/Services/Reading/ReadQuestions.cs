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
            Console.WriteLine(sentences[sentenceAt]);
            WordsOfQuestions = sentences[sentenceAt].Split(' ');
            Console.WriteLine(WordsOfQuestions.Length);
            
            for(int i = positionAt; i < WordsOfQuestions.Length; i++)
            {
                await Task.Delay(WordsOfQuestions[i].Length * 40);
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
                    await Task.Delay(800);
                    qVm.QuestionText = "";
                    ReadAQuestion(qVm);
                }
                else
                {
                    qVm.IsReading = false;
                }
            }

        }
    }
}

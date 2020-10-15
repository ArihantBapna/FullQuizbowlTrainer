using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.Services.Database;

namespace FullQuizbowlTrainer.Services.Selector
{
    public class NewAnswerLine
    {
        public Answers SelectedAnswer { get; set; }
        public Questions SelectedQuestion { get; set; }
        

        public NewAnswerLine(UserProfile userProfile)
        {
            Categories SelectedCategory = SelectNewCategory(userProfile.Categories);
            SelectedAnswer = SelectNewAnswer(SelectedCategory, userProfile.Answers);
        }

        public Categories SelectNewCategory(ObservableCollection<Categories> CatData)
        {
            double probability = new Random().NextDouble()*100;
            Dictionary<Categories, RangeClass> Sorted = new Dictionary<Categories, RangeClass>();
            double lastVal = 0;
            foreach(Categories c in CatData)
            {
                RangeClass rangeClass = new RangeClass();
                rangeClass.start = lastVal;
                rangeClass.end = lastVal + c.Percent;
                Sorted.Add(c, rangeClass);
                lastVal += c.Percent;
            }
            foreach(KeyValuePair<Categories,RangeClass> pair in Sorted)
            {
                RangeClass rangeClass = pair.Value;
                if(rangeClass.end >= probability && rangeClass.start <= probability)
                {
                    return pair.Key;
                }
            }
            return CatData[0];
        }

        public Answers SelectNewAnswer(Categories SelectedCat, ObservableCollection<Answers> ans)
        {
            List<Answers> SubList = new List<Answers>();
            foreach(Answers a in ans)
            {
                if(a.Category == SelectedCat.Id)
                {
                    if(a.Rating > (SelectedCat.User-150) && a.Rating < (SelectedCat.User+250))
                    {
                        SubList.Add(a);
                    }
                }
            }
            
            SubList.Sort((x, y) => x.Score.CompareTo(y.Score));
            Random r = new Random();
            int newR = 0;
            if (SubList.Count > 10)
            {
                 newR = r.Next(SubList.Count - 10, SubList.Count - 1);
            }
            else
            {
                newR = r.Next(0, SubList.Count - 1);
            }
            return SubList[newR];
        }

        public async Task<Questions> GetNewQuestion(Answers SelectedAnswer)
        {
            DatabaseManager dbM = new DatabaseManager();
            List<Questions> availQuestions = await dbM.GetQuestionsFromAnswerId(SelectedAnswer.ID);
            availQuestions.Sort((x, y) => x.Answered.CompareTo(y.Answered));

            Random r = new Random();
            int newR = r.Next(0, availQuestions.Count);
            SelectedQuestion = availQuestions[newR];
            return SelectedQuestion;
        }
    }
}

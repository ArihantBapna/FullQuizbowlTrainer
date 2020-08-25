using System;
namespace FullQuizbowlTrainer.Models
{
    public class AnswersModel
    {
        public string Answer { get; set; }
        public int Category { get; set; }
        public int Corrects { get; set; }
        public int Difficulty { get; set; }
        public int ID { get; set; }
        public int Negs { get; set; }
        public double Score { get; set; }
        public int Size { get; set; }
    }
}

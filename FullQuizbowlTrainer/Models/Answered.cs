using System;
namespace FullQuizbowlTrainer.Models
{
    public class Answered
    {
        public int IndexID { get; set; }
        public int AnswerID { get; set; }
        public string Answer { get; set; }
        public int Category { get; set; }
        public int Corrects { get; set; }
        public int Difficulty { get; set; }
        public double Rating { get; set; }
        public int Negs { get; set; }
        public double Score { get; set; }
    }
}

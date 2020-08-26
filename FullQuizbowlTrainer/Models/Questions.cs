using System;
namespace FullQuizbowlTrainer.Models
{
    public class Questions
    {
        public string Alternate { get; set; }
        public string Answer { get; set; }
        public int AnswerID { get; set; }
        public int Category { get; set; }
        public int Difficulty { get; set; }
        public int ID { get; set; }
        public string Prompt { get; set; }
        public string Question { get; set; }
        public int Subcategory { get; set; }
        public string Tournament { get; set; }
        public int TournamentID { get; set; }
    }
}

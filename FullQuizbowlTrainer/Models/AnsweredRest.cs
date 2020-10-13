using System;
namespace FullQuizbowlTrainer.Models
{
    public class AnsweredRest
    {
        public int userid { get; set; }
        public int answerid { get; set; }
        public int questionid { get; set; }
        public string buzzed { get; set; }
        public string clue { get; set; }
        public double score { get; set; }
        public double rating { get; set; }
    }
}

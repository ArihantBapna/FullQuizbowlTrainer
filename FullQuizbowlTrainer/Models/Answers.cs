using System;
using SQLite;

namespace FullQuizbowlTrainer.Models
{
    [Table("Answers")]
    public class Answers
    {
        public string Answer { get; set; }
        public int Category { get; set; }
        public int Corrects { get; set; }
        public int Difficulty { get; set; }

        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }


        public double Rating { get; set; }
        public int Negs { get; set; }
        public double Score { get; set; }
        public int Size { get; set; }
    }
}

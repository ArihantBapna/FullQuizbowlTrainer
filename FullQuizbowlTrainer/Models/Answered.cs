using System;
using System.Reflection;
using System.Text;
using SQLite;

namespace FullQuizbowlTrainer.Models
{
    public class Answered
    {
        [PrimaryKey, AutoIncrement]
        public int IndexID { get; set; }

        public int AnswerID { get; set; }
        public string Answer { get; set; }
        public int Category { get; set; }
        public int Difficulty { get; set; }
        public double Rating { get; set; }
        public int Negs { get; set; }
        public double Score { get; set; }
        public int QuestionID { get; set; }
        public string Correct { get; set; }

        private PropertyInfo[] _PropertyInfos = null;

        public override string ToString()
        {
            if (_PropertyInfos == null)
                _PropertyInfos = this.GetType().GetProperties();

            var sb = new StringBuilder();

            foreach (var info in _PropertyInfos)
            {
                var value = info.GetValue(this, null) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value.ToString());
            }

            return sb.ToString();
        }

    }
}

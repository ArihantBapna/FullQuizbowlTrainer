using System;
using System.Collections.ObjectModel;

namespace FullQuizbowlTrainer.Models
{
    public class UserProfile
    {
        public ObservableCollection<Answers> Answers { get; set; }
        public ObservableCollection<Categories> Categories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FullQuizbowlTrainer.Models;
using Syncfusion.SfChart.XForms;

namespace FullQuizbowlTrainer.ViewModels
{
    public class SetCategoriesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Categories> catData = new ObservableCollection<Categories>();
        public ObservableCollection<Categories> CategoryData
        {
            get { return catData; }
            set
            {
                catData = value;
                OnPropertyChanged("CategoryData");
            }
        }

        public SetCategoriesViewModel(List<Categories> categoryDat)
        {
            CategoryData = new ObservableCollection<Categories>(categoryDat);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}

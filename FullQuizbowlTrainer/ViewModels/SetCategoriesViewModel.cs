using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FullQuizbowlTrainer.Models;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;

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
                CalculateTotal();
            }
        }

        public void Subscribe()
        {
            MessagingCenter.Subscribe<Categories>(this, "Hi", (sender) => {
                CalculateTotal();
                Console.WriteLine("Calculated total");
            });
        }

        private double totalAvailable;
        public double TotalAvailable
        {
            get { return totalAvailable; }
            set
            {
                totalAvailable = value;
                OnPropertyChanged("TotalAvailable");
            }
        }

        public SetCategoriesViewModel(List<Categories> categoryDat)
        {
            CategoryData = new ObservableCollection<Categories>(categoryDat);
            CalculateTotal();
        }
        

        public void CalculateTotal()
        {
            double t = 0;
            foreach(Categories c in CategoryData)
            {
                t += c.Percent;
            }
            TotalAvailable = 100-t;
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}

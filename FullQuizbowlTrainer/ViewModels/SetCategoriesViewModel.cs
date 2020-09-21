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
            MessagingCenter.Subscribe<Categories>(this, "UpdateTotal", (sender) => {
                CalculateTotal();
                Console.WriteLine("Calculated total");
            });
        }

        public static double Total = 0;

        private double totalAvailable;
        public double TotalAvailable
        {
            get { return totalAvailable; }
            set
            {
                totalAvailable = value;
                OnPropertyChanged("TotalAvailable");
                Total = TotalAvailable;
            }
        }

        public static void ClearContents(SetCategoriesViewModel vm)
        {
            ObservableCollection<Categories> vcm = new ObservableCollection<Categories>();
            foreach(Categories c in vm.CategoryData)
            {
                Categories c2 = new Categories();
                c2.Name = c.Name;
                c2.Percent = 0;
                vcm.Add(c2);
            }
            vm.CategoryData = vcm;
            vm.CalculateTotal();
        }

        public SetCategoriesViewModel(List<Categories> categoryDat)
        {
            CategoryData = new ObservableCollection<Categories>(categoryDat);
            CalculateTotal();
            Subscribe();
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

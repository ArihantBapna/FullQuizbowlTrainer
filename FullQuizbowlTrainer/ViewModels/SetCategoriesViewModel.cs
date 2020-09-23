using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FullQuizbowlTrainer.Models;
using Syncfusion.SfChart.XForms;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.ViewModels
{
    public class SetCategoriesViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Presets> presData = new ObservableCollection<Presets>();
        public ObservableCollection<Presets> PresetsData
        {
            get { return presData; }
            set
            {
                presData = value;
                OnPropertyChanged("PresetsData");
            }
        }

        public List<Categories> CategoryData { get; set; } 

        private PreferenceId Preference { get; set; }

        private string preferenceName;
        public string PreferenceName
        {
            get { return preferenceName; }
            set
            {
                preferenceName = value;
                OnPropertyChanged("PreferenceName");
            }
        }

        public void Subscribe()
        {
            MessagingCenter.Subscribe<Presets>(this, "UpdateTotal", (sender) => {
                CalculateTotal();
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
            ObservableCollection<Presets> vcm = new ObservableCollection<Presets>();
            foreach(Presets c in vm.PresetsData)
            {
                Presets c2 = new Presets();
                c2.Name = c.Name;
                c2.Percent = 0;
                vcm.Add(c2);
            }
            vm.PresetsData = vcm;
            vm.CalculateTotal();
        }

        public SetCategoriesViewModel(PreferenceId pref, List<Categories> categData, int action)
        {
            if(action == 0)
            {
                PreferenceId prefNew = new PreferenceId();
                prefNew.Name = "New Name";
                prefNew.PresetData = "12,17,7,34,5,5,12,2,4,2";

                string default_key = "name=Default,id=0,12,17,7,17,5,5,12,2,4,2;name=Second,id=1,14,15,7,17,5,5,12,2,4,2";
                string keyVal = Preferences.Get("pref_keys", default_key);

                int id = 0;
                foreach (string s in keyVal.Split(';'))
                {
                    foreach (string t in s.Split(','))
                    {
                        if (t.Contains("id="))
                        {
                            string u = t.Replace("id=", "");
                            int.TryParse(u, out id);
                        }
                    }
                }
                prefNew.Id = id + 1;
                Preference = prefNew;
                PreferenceName = Preference.Name;
            }else if(action == 1)
            {
                Preference = pref;
                PreferenceName = Preference.Name;
            }

            CategoryData = categData;
            SetChartValues(Preference.PresetData);
            CalculateTotal();
            Subscribe();
        }

        private void SetChartValues(string presets)
        {
            int count = 0;
            List<Presets> presetsDat = new List<Presets>();
            foreach(string p in presets.Split(','))
            {
                double percent = 0;

                Presets pr = new Presets();
                pr.Name = CategoryData[count].Name;
                double.TryParse(p, out percent);
                pr.Percent = percent;

                presetsDat.Add(pr);

                count++;
            }
            PresetsData = new ObservableCollection<Presets>(presetsDat);
        }

        public void CalculateTotal()
        {
            double t = 0;
            foreach(Presets c in PresetsData)
            {
                t += c.Percent;
            }
            TotalAvailable = 100-t;
        }

        public static async void SaveNewPreference(SetCategoriesViewModel vm, INavigation Nav)
        {

            string default_key = "name=Default,id=0,12,17,7,17,5,5,12,2,4,2;name=Second,id=1,14,15,7,17,5,5,12,2,4,2";
            string keyVal = Preferences.Get("pref_keys", default_key);

            string newKey = "name=" + vm.PreferenceName + ",id=" + vm.Preference.Id +",";

            Presets lastP = vm.PresetsData[vm.PresetsData.Count-1];
            foreach(Presets p in vm.PresetsData)
            {
                if (p != lastP)
                {
                    newKey += p.Percent + ",";
                }
                else
                {
                    newKey += p.Percent;
                }
            }

            vm.Preference.PresetData = newKey;
            keyVal = keyVal + ";" + newKey;
            Preferences.Set("pref_keys", keyVal);
            MessagingCenter.Send(vm, "UpdatePresets");
            await Nav.PopModalAsync();
        }

        
        

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}

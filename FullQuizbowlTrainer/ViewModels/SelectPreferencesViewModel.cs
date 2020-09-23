using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FullQuizbowlTrainer.Models;
using FullQuizbowlTrainer.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.ViewModels
{
    public class SelectPreferencesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Categories> categoryDat;
        public ObservableCollection<Categories> CategoryData
        {
            get { return categoryDat; }
            set
            {
                categoryDat = value;
                OnPropertyChanged("CategoryData");
            }
        }

        private ObservableCollection<Presets> presetDat;
        public ObservableCollection<Presets> PresetData
        {
            get { return presetDat; }
            set
            {
                presetDat = value;
                OnPropertyChanged("PresetData");
            }
        }

        private ObservableCollection<PreferenceId> savedPreferences;
        public ObservableCollection<PreferenceId> SavedPreferences
        {
            get { return savedPreferences; }
            set
            {
                savedPreferences = value;
                OnPropertyChanged("SavedPreferences");
            }
        }

        private PreferenceId selectedPreference;
        public PreferenceId SelectedPreference
        {
            get { return selectedPreference; }
            set
            {
                selectedPreference = value;
                OnPropertyChanged("SelectedPreference");
                if(SelectedPreference != null)
                {
                    HasSelectedItem = true;
                    SetChartValues();
                }
                else
                {
                    HasSelectedItem = false;
                    PresetData = new ObservableCollection<Presets>();
                }
            }
        }
        private bool hasSelectedItem;
        public bool HasSelectedItem
        {
            get { return hasSelectedItem; }
            set
            {
                hasSelectedItem = value;
                OnPropertyChanged("HasSelectedItem");
                NotSelectedItem = !HasSelectedItem;
            }
        }

        private bool notSelectedItem;
        public bool NotSelectedItem
        {
            get { return notSelectedItem; }
            set
            {
                notSelectedItem = value;
                OnPropertyChanged("NotSelectedItem");
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        private void Subscribe()
        {
            MessagingCenter.Subscribe<SetCategoriesViewModel>(this,"UpdatePresets", (sender) => {
                SetKeyValues();
            });
        }

        public SelectPreferencesViewModel(List<Categories> categoryData)
        {
            categoryDat = new ObservableCollection<Categories>(categoryData);
            
            SetKeyValues();
            Subscribe();

            IsLoading = false;

        }

        private void SetKeyValues()
        {
            string default_key = "name=Default,id=0,12,17,7,17,5,5,12,2,4,2;name=Second,id=1,14,15,7,17,5,5,12,2,4,2";
            string keyVal = Preferences.Get("pref_keys", default_key);
            HasSelectedItem = false;
            GetPreferences(keyVal);
           
        }

        private void SetChartValues()
        {
            int count = 0;
            string[] vals = SelectedPreference.PresetData.Split(',');
            List<Presets> pres_val = new List<Presets>();
            foreach(string v in vals)
            {
                Presets p = new Presets();
                p.Name = CategoryData[count].Name;
                double percent = 0;
                double.TryParse(v, out percent);
                p.Percent = percent;

                pres_val.Add(p);

                count++;
            }

            PresetData = new ObservableCollection<Presets>(pres_val);
        }

        private void GetPreferences(string keyVal)
        {
            string[] indiv_prefs = keyVal.Split(';');
            List<PreferenceId> prefs_ids = new List<PreferenceId>();
            
            foreach (string s in indiv_prefs)
            {
                string[] indiv_internal = s.Split(',');
                int i = 0;

                PreferenceId pref_id = new PreferenceId();


                foreach(string t in indiv_internal)
                {
                    if (i == 0)
                    {
                        string t1 = t.Remove(0, 5);
                        string name = t1;
                        pref_id.Name = name;
                    }
                    else if (i == 1)
                    {
                        int id = 0;
                        string t1 = t.Remove(0, 3);
                        int.TryParse(t1, out id);
                        pref_id.Id = id;
                    }
                    else
                    {
                        pref_id.PresetData += t;
                        if (i != indiv_internal.Length-1)
                        {
                            pref_id.PresetData += ",";
                        }
                    }
                    i++;
                }

                prefs_ids.Add(pref_id);
            }
            SavedPreferences = new ObservableCollection<PreferenceId>(prefs_ids);
        }

        public async static void PushCategoriesModal(SelectPreferencesViewModel vm, INavigation navigation, int action)
        {
            vm.IsLoading = true;

            await navigation.PushModalAsync(new NavigationPage(new SetCategories(vm.SelectedPreference,new List<Categories>(vm.CategoryData),navigation,action)));

            vm.IsLoading = false;
        }

        public static void DeletePresetData(SelectPreferencesViewModel vm)
        {
            vm.IsLoading = true;

            PreferenceId pref = vm.SelectedPreference;

            string default_key = "name=Default,id=0,12,17,7,17,5,5,12,2,4,2;name=Second,id=1,14,15,7,17,5,5,12,2,4,2";
            string keyVal = Preferences.Get("pref_keys", default_key);

            string keyToDelete = "name=" + pref.Name + ",id=" + pref.Id + "," + pref.PresetData;
            string finalKey = "";
            foreach (string s in keyVal.Split(';'))
            {
                if (!s.Equals(keyToDelete))
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        if (finalKey.Equals("")) finalKey += s;
                        else finalKey += ";" + s;
                    }
                }
            }

            Preferences.Set("pref_keys", finalKey);
            vm.SetKeyValues();

            vm.SavedPreferences.Remove(pref);

            vm.IsLoading = false;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

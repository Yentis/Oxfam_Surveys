using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OxfamSurveys.Messages;
using OxfamSurveys.Models;
using OxfamSurveys.Models.KoBoApiRequests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly KoBoApi api = new KoBoApi(new ApiConfig().Get(Apis.KoBoCollect));
        private readonly FoodList foodList = new FoodList();

        #region Private attributes
        public Form _SelectedForm;
        public string _FormName;
        #endregion

        #region Public attributes
        // TODO: Should be wrapped to make a standard interface for every API
        public ObservableCollection<Form> Forms { get; } = new ObservableCollection<Form>();

        public Form SelectedForm
        {
            get
            {
                return _SelectedForm;
            }
            set
            {
                if (_SelectedForm != value)
                {
                    _SelectedForm = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string FormName
        {
            get
            {
                return _FormName;
            }
            set
            {
                if (value != _FormName)
                {
                    _FormName = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        public MenuViewModel()
        {
            UpdateFood();
            MessengerInstance.Register<FormsChanged>(this, message => {
                api.SetConfig(new ApiConfig().Get(Apis.KoBoCollect));
                UpdateFood();
            });
        }

        private void UpdateFood()
        {
            Forms.Clear();
            api.GetForms().ForEach(form => Forms.Add(form));

            if (Forms.Count > 0)
            {
                SelectedForm = Forms[0];
            }
        }

        #region Commands
        private ICommand _CreateFormCommand;
        public ICommand CreateFormCommand
        {
            get
            {
                return _CreateFormCommand ?? (
                    _CreateFormCommand = new RelayCommand(() =>
                    {
                        /*
                        ExcelFile = new Excel("NutVal.xlsm", "Database");
                        List<Food> food = excelFile.ReadData();
                        List<FoodAmount> foodamounts = new List<FoodAmount>();
                        //foodamounts.Add(new FoodAmount(foods[5], 200));
                        Random rand = new Random();
                        for(int i = 0; i < 20; i++)
                        {
                            foodamounts.Add(new FoodAmount(foods[rand.Next(0, foods.Count-1)], rand.Next(5, 100)));
                        }
                        ExcelFile.SetWorkSheet("Calculation Sheet");
                        ExcelFile.WriteData(foodamounts);
                        */
                        
                        try
                        {
                            List<Food> food = foodList.Get();
                            XLSForm form = new XLSForm();
                            string path = form.Generate(food);
                            
                            var apiForm = api.CreateForm(FormName, path);

                            MessengerInstance.Send(new FormsChanged());

                            MessageBox.Show("Form created successfully! URL: " + apiForm.Url, "Success!");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Exception: " + e.Message, "Error");
                        }
                    })
                );
            }
        }

        private ICommand _DownloadNutValCommand;
        public ICommand DownloadNutValCommand
        {
            get
            {
                return _DownloadNutValCommand ?? (
                    _DownloadNutValCommand = new RelayCommand(() =>
                    {
                        var foodDictionary = new Dictionary<Food, List<float>>();

                        foreach (FormLine line in api.GetData(SelectedForm.Formid).Lines)
                        {
                            MessageBox.Show(line.Food + ": " + line.Amount + " - " + line.Origin);

                            if (!foodDictionary.ContainsKey(line.Food))
                            {
                                foodDictionary.Add(line.Food, new List<float>());
                            }

                            foodDictionary[line.Food].Add(line.Amount);
                        }

                        List<FoodAmount> foodList = new List<FoodAmount>();

                        foreach (KeyValuePair<Food, List<float>> line in foodDictionary)
                        {
                            foodList.Add(new FoodAmount(line.Key, line.Value.Average()));
                        }

                        Excel excel = new Excel("NutVal.xlsm", "Database");
                        excel.SetWorkSheet("Calculation Sheet");
                        excel.WriteData(foodList);
                        excel.ExcelApp.Visible = true;
                    })
                );
            }
        }
        #endregion
    }
}

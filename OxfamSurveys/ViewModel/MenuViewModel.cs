using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MongoDB.Bson;
using MongoDB.Driver;
using OxfamSurveys.Extensions;
using OxfamSurveys.Messages;
using OxfamSurveys.Models;
using OxfamSurveys.Models.KoBoApiRequests;
using System;
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
        private Form _SelectedForm;
        private string _FormName;

        private DateTime _BeginDate = DateTime.Now.AddDays(-7);
        private DateTime _EndDate = DateTime.Now.AddDays(1);
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

        public DateTime BeginDate
        {
            get
            {
                return _BeginDate;
            }
            set
            {
                if (value != _BeginDate)
                {
                    _BeginDate = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                if (value != _EndDate)
                {
                    _EndDate = value;
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
                        if (FormName == null)
                        {
                            MessageBox.Show("Please enter a name for the form.");
                            return;
                        }

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

                        var builder = new FilterDefinitionBuilder<BsonDocument>();
                        var filter = builder.Gte("_submission_time", BeginDate.ToString("o")) & builder.Lte("_submission_time", EndDate.ToString("o"));
                        var jsonQuery = filter.RenderToBsonDocument().ToJson();

                        FormData data = api.GetData(SelectedForm.Formid, jsonQuery);

                        if (data.Lines.Count() == 0)
                        {
                            MessageBox.Show("No data available");
                            return;
                        }

                        foreach (FormLine line in data.Lines)
                        {
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

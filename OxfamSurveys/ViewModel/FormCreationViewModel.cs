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
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class FormCreationViewModel : ViewModelBase
    {
        private readonly KoBoApi api = new KoBoApi(new ApiConfig().Get(Apis.KoBoCollect));
        private readonly FoodList foodList = new FoodList();

        #region Private attributes
        private string _FormName;
        
        private string _FormContent = "Create online!";
        private bool _FormEnabled = true;
        #endregion

        #region Public attributes
        // TODO: Should be wrapped to make a standard interface for every API
        public ObservableCollection<Form> Forms { get; } = new ObservableCollection<Form>();
        
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

        public string FormContent
        {
            get
            {
                return _FormContent;
            }
            set
            {
                _FormContent = value;
                RaisePropertyChanged();
            }
        }

        public bool FormEnabled
        {
            get
            {
                return _FormEnabled;
            }

            set
            {
                _FormEnabled = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public void CreateXLSForm()
        {
            FormEnabled = false;
            FormContent = "Loading...";

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

            FormEnabled = true;
            FormContent = "Create online!";

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

                        Thread newThread = new Thread(CreateXLSForm);
                        newThread.Start();
                    })
                );
            }
        }
        #endregion
    }
}

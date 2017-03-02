using OxfamSurveys.Models.KoBoApiRequests;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;

namespace OxfamSurveys.Models
{
    public class KoBoApi : Api
    {
        private const string baseUrl = "https://kc.kobotoolbox.org/api/v1/";

        private string username;
        private string password;
        private string server;

        private List<Food> food;

        public KoBoApi(string username, string password, string server = "")
        {
            this.username = username;
            this.password = password;
            this.server = server;
        }

        public bool CreateForm(string name, IEnumerable<Food> food)
        {
            throw new NotImplementedException();
        }

        public FormData GetData(object formId)
        {
            var request = new RestRequest();
            request.Resource = "data/" + formId.ToString();

            List<FormLine> lines = new List<FormLine>();
            List<Data> data = Execute<List<Data>>(request);
            foreach (var form in data)
            {
                if (form.Nutval == null)
                {
                    continue;
                }

                foreach (var line in form.Nutval)
                {
                    Food food = GetFoodById(line.Food);
                    FormLine formLine = new FormLine(food, line.Quantity, Origins.GetById(line.Origin));
                }
            }

            return new FormData(0, lines);
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            // Always request data in JSON format
            request.AddParameter("format", "json");

            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);
            client.Authenticator = new HttpBasicAuthenticator(username, password);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return response.Data;
        }

        public List<Form> GetForms()
        {
            var request = new RestRequest();
            request.Resource = "forms";

            return Execute<List<Form>>(request);
        }

        private Food GetFoodById(int id)
        {
            if (food == null)
            {
                var excel = new Excel("NutVal.xlsm", "Database");
                food = excel.ReadData();
            }
            
            return food[id];
        }
    }
}

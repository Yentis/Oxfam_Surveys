using OxfamSurveys.Models.KoBoApiRequests;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using static OxfamSurveys.Models.ApiConfig;

namespace OxfamSurveys.Models
{
    public class KoBoApi : Api
    {
        private const string baseUrl = "https://kc.kobotoolbox.org/api/v1/";

        private string username;
        private string password;
        private string server;

        private List<Food> food;

        public KoBoApi()
        {
            Config config = new ApiConfig().Get(Apis.KoBoCollect);
            username = config.Username;
            password = config.Password;
            server = config.Server;
        }

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
                    lines.Add(new FormLine(GetFoodById(line.Food), line.Quantity, Origins.GetById(line.Origin)));
                }
            }

            return new FormData(0, lines);
        }

        public List<Form> GetForms()
        {
            var request = new RestRequest();
            request.Resource = "forms";

            return Execute<List<Form>>(request);
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

        private Food GetFoodById(int id)
        {
            if (food == null)
            {
                food = new FoodList().Get();
            }

            return food[id];
        }
    }
}

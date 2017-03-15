using OxfamSurveys.Models.KoBoApiRequests;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using OxfamSurveys.Extensions;
using static OxfamSurveys.Models.ApiConfig;
using System.IO;
using RestSharp.Deserializers;

namespace OxfamSurveys.Models
{
    public class KoBoApi : Api
    {
        private const string baseUrl = "https://kc.kobotoolbox.org/api/v1/";

        private string username;
        private string password;
        private string server;

        private List<Food> food;

        public KoBoApi(Config config)
        {
            SetConfig(config);
        }

        public void SetConfig(Config config)
        {
            username = config.Username;
            password = config.Password;
            server = config.Server;
        }

        public Form CreateForm(string name, string path)
        {
            var request = new RestRequest("forms", Method.POST);
            request.AddFile("xls_file", File.ReadAllBytes(path), name.GenerateSlug() + ".xls");

            var form = Execute<Form>(request);

            request = new RestRequest("forms/" + form.Formid + "/labels", Method.POST);
            request.AddParameter("tags", "nutval");
            
            Execute<object>(request);

            request = new RestRequest("forms/" + form.Formid, Method.PATCH);
            request.AddParameter("title", name);

            return Execute<Form>(request);
        }

        public FormData GetData(object formId, object query = null)
        {
            var request = new RestRequest("data/" + formId.ToString());
            request.AddQueryParameter("query", query.ToString());

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
                    lines.Add(new FormLine(GetFoodById(line.Food), line.Quantity / form.PeopleNbr, Origins.GetById(line.Origin)));
                }
            }

            return new FormData(lines);
        }

        public List<Form> GetForms()
        {
            var request = new RestRequest("forms");
            request.AddQueryParameter("tags", "nutval");

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

            // Check if the JSON response contains an error (JSON keys "type" and "text")
            // If not, we don't care (empty catch) as the response is probably valid and
            // deserialized correctly by the generic type.
            Response responseObject = null;

            try
            {
                var deserializer = new JsonDeserializer();
                responseObject = deserializer.Deserialize<Response>(response);
            }
            catch (Exception)
            {

            }
            
            if (responseObject != null && responseObject.Type == "alert-error")
            {
                throw new Exception(responseObject.Text);
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

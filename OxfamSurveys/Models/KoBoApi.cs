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

        public KoBoApi(string username, string password, string server = "")
        {
            this.username = username;
            this.password = password;
            this.server = server;
        }

        public bool createForm(string name, IEnumerable<Food> food)
        {
            throw new NotImplementedException();
        }

        public FormData getData()
        {
            return new FormData(0, new List<FormLine>());
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
    }
}

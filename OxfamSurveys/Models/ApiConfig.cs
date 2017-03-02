using IniParser;
using IniParser.Exceptions;
using IniParser.Model;

namespace OxfamSurveys.Models
{
    class ApiConfig
    {
        private static readonly string FILE_NAME = "api-config.ini";
        private FileIniDataParser parser = new FileIniDataParser();

        public void Set(Apis api, string username, string password, string server)
        {
            KeyDataCollection collection = new KeyDataCollection();
            collection["username"] = username;
            collection["password"] = password;
            collection["server"] = server;

            IniData config;
            try
            {
                config = Get();
            }
            catch (ParsingException)
            {
                config = new IniData();
            }

            config[api.ToString()].Merge(collection);
            parser.WriteFile(FILE_NAME, config);
        }

        public Config Get(Apis api)
        {
            KeyDataCollection config = Get()[api.ToString()];
            return new Config(api, config["username"], config["password"], config["server"]);
        }

        private IniData Get()
        {
            return parser.ReadFile(FILE_NAME);
        }

        internal class Config
        {
            public Apis Api { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Server { get; set; }

            public Config(Apis api, string username, string password, string server)
            {
                Api = api;
                Username = username;
                Password = password;
                Server = server;
            }
        }
    }
}

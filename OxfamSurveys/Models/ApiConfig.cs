﻿using IniParser;
using IniParser.Exceptions;
using IniParser.Model;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace OxfamSurveys.Models
{
    public class ApiConfig
    {
        private static readonly string FILE_NAME = "api-config.ini";
        private FileIniDataParser parser = new FileIniDataParser();

        private IniData Data;

        public void Set(Apis api, string username, SecureString password, string server)
        {
            KeyDataCollection collection = new KeyDataCollection();
            collection["username"] = username;
            if(password != null)
            {
                collection["password"] = SecureStringToString(password);
            }
            collection["server"] = server;

            IniData config = Get();
            config[api.ToString()].Merge(collection);
            parser.WriteFile(FILE_NAME, config);
            Data = config;
        }

        static string SecureStringToString(SecureString value)
        {
            IntPtr bstr = Marshal.SecureStringToBSTR(value);

            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
        }

        public Config Get(Apis api)
        {
            KeyDataCollection config = Get()[api.ToString()];
            return new Config(api, config["username"], config["password"], config["server"]);
        }

        private IniData Get()
        {
            if (Data == null)
            {
                try
                {
                    Data = parser.ReadFile(FILE_NAME);
                }
                catch (ParsingException)
                {
                    Data = new IniData();
                }
            }

            return Data;
        }

        public class Config
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

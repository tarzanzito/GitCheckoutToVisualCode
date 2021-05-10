
using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Candal.Configuration
{
    public class AppConfigRootJson
    {
        private AppConfigData _configData;

        [JsonProperty("App")]
        public AppConfigData ConfigData
        {
            get { return _configData; }
            set { _configData = value; }
        }

        public AppConfigRootJson()
        {
            _configData = new AppConfigData();
        }
    }

    public class AppConfigJson : IAppConfig
    {
        private readonly string _fullExeFileName;
        private readonly string _fullConfigFileName;
        private readonly string _appConfigName;
        private AppConfigRootJson _appConfigRootJson;

        public AppConfigData Data
        {
            get { return _appConfigRootJson.ConfigData; }
        }

        public AppConfigJson()
        {
            _fullExeFileName = Assembly.GetEntryAssembly().Location;
            _fullConfigFileName = $"{_fullExeFileName}.json";
            _appConfigName = $"{Assembly.GetEntryAssembly().GetName().Name}.json";
        }

        private void Create()
        {
            _appConfigRootJson = new AppConfigRootJson();
        }

        public void Load()
        {
            try
            {
                if (!File.Exists(_fullConfigFileName))
                    throw new FileNotFoundException($"File [{_appConfigName}] not found.");

                string jsonText = File.ReadAllText(_fullConfigFileName);
                _appConfigRootJson = JsonConvert.DeserializeObject<AppConfigRootJson>(jsonText);

                if (_appConfigRootJson == null)
                    throw new Exception("'AppConfigRootJson' class is null.");

                _appConfigRootJson.ConfigData.Decrypt();
            }
            catch (Exception ex)
            {
                Create();
            }
        }

        public void Save()
        {
            try
            {
                if (_appConfigRootJson == null)
                    throw new Exception("'AppConfigRootJsonFile' class is null.");

                _appConfigRootJson.ConfigData.Encrypt();

                string jsonText = JsonConvert.SerializeObject(_appConfigRootJson);

                using (StreamWriter writer = File.CreateText(_fullConfigFileName))
                {
                    writer.WriteLine(jsonText);
                }
            }
            catch (Exception ex)
            {
                _appConfigRootJson = new AppConfigRootJson(); //rever
                //todo: log
            }

        }
    }
}
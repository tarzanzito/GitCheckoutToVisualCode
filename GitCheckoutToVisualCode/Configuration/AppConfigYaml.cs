
using System;
using System.Reflection;
using System.IO;
using YamlDotNet.Serialization;

namespace Candal.Configuration
{
    public class AppConfigRootYaml
    {
        private AppConfigData _configData;

        [YamlMember(Alias = "App")]
        public AppConfigData ConfigData
        {
            get { return _configData; }
            set { _configData = value; }
        }

        public AppConfigRootYaml()
        {
            _configData = new AppConfigData();
        }
    }

    public class AppConfigYaml : IAppConfig
    {
        private readonly string _fullExeFileName;
        private readonly string _fullConfigFileName;
        private readonly string _appConfigName;
        private AppConfigRootYaml _appConfigRootYaml;

        public AppConfigData Data
        {
            get { return _appConfigRootYaml.ConfigData; }
        }

        public AppConfigYaml()
        {
            _fullExeFileName = Assembly.GetEntryAssembly().Location;
            _fullConfigFileName = $"{_fullExeFileName}.yml";
            _appConfigName = $"{Assembly.GetEntryAssembly().GetName().Name}.yml";
        }

        public void Create()
        {
            _appConfigRootYaml = new AppConfigRootYaml();
        }

        public void Load()
        {
            try
            {
                if (!File.Exists(_fullConfigFileName))
                    throw new FileNotFoundException($"File [{_appConfigName}] not found.");

                IDeserializer deserializer = new DeserializerBuilder().Build();
                string yamlText = System.IO.File.ReadAllText(_fullConfigFileName);

                _appConfigRootYaml = deserializer.Deserialize<AppConfigRootYaml>(yamlText);

                if (_appConfigRootYaml == null)
                    throw new Exception($"File [{_fullConfigFileName}] is empty or with invalid format.");

                _appConfigRootYaml.ConfigData.Decrypt();
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
                if (_appConfigRootYaml == null)
                    throw new Exception("'App Config Root Yaml' is null.");

                _appConfigRootYaml.ConfigData.Encrypt();

                ISerializer serializer = new SerializerBuilder().Build();
                string yamlText = serializer.Serialize(_appConfigRootYaml);

                using (StreamWriter writer = File.CreateText(_fullConfigFileName))
                {
                    writer.WriteLine(yamlText);
                }
            }
            catch (Exception ex)
            {
                _appConfigRootYaml = new AppConfigRootYaml();
                //todo: log
            }

        }
    }
}
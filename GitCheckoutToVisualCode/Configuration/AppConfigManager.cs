
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Candal.Configuration
{
    public class AppConfig : IAppConfig
    {
        private readonly string _fullExeFileName;
        private readonly string _fullConfigFileName;
        private readonly string _appConfigName;
        private System.Configuration.Configuration _config;
        private KeyValueConfigurationCollection _settings;
        private AppConfigData _appConfigData;

        public AppConfigData Data
        {
            get { return _appConfigData; }
        }

        public AppConfig()
        {
            _fullExeFileName = Assembly.GetEntryAssembly().Location;
            _fullConfigFileName = $"{_fullExeFileName}.config";
            _appConfigName = $"{Assembly.GetEntryAssembly().GetName().Name}.config";
        }

        public void Load()
        {
            try
            {
                if (!File.Exists(_fullConfigFileName))
                    throw new FileNotFoundException($"File [{_appConfigName}] not found.");

                _config = ConfigurationManager.OpenExeConfiguration(_fullExeFileName);
                //var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                _settings = _config.AppSettings.Settings;
                _appConfigData = new AppConfigData();

                _appConfigData.GitUrl = GetItemToString("GitUrl");
                _appConfigData.UseSSH = GetItemToBoolean("UseSSH");
                _appConfigData.UserName = GetItemToString("UserName");
                _appConfigData.Password = GetItemToString("Password");
                _appConfigData.RootFolder = GetItemToString("RootFolder");

                _appConfigData.Decrypt();
            }
            catch (Exception ex)
            {
                Create();
                //todo: log
            }
        }

        public void Save()
        {
            try
            {
                if (!File.Exists(_fullConfigFileName) || _settings == null)
                {
                    Create();
                    _config = ConfigurationManager.OpenExeConfiguration(_fullExeFileName);
                    _settings = _config.AppSettings.Settings;
                }

                _appConfigData.Encrypt();

                SetItem("GitUrl", _appConfigData.GitUrl);
                SetItem("UseSSH", _appConfigData.UseSSH.ToString().Trim());
                SetItem("UserName", _appConfigData.UserName);
                SetItem("Password", _appConfigData.Password);
                SetItem("RootFolder", _appConfigData.RootFolder);

                _config.Save(ConfigurationSaveMode.Modified);
                //_config.Save(ConfigurationSaveMode.Minimal);

                ConfigurationManager.RefreshSection(_config.AppSettings.SectionInformation.Name);
            }
            catch (Exception ex)
            {
                _appConfigData = new AppConfigData();
                //todo: log
            }
        }

        private void Create()
        {
            try
            {
                string data = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><configuration></configuration>";
                File.WriteAllText(_fullConfigFileName, data);
                _appConfigData = new AppConfigData();
            }
            catch (Exception ex)
            {
            }
        }

        private string GetItemToString(string key, string defaultValue = "")
        {
            if (_settings[key] == null)
                return defaultValue;
            else
                return _settings[key].Value.Trim();
        }

        private bool GetItemToBoolean(string key, bool defaultValue = false)
        {
            if (_settings[key] == null)
                return defaultValue;

            try
            {
                string temp = _settings[key].Value.Trim();
                return System.Convert.ToBoolean(temp);
            } catch
            {
                return defaultValue;
            }
        }

        private int GetItemToInteger(string key, int defaultValue = 0)
        {
            if (_settings[key] == null)
                return defaultValue;

            try
            {
                string temp = _settings[key].Value.Trim();
                return System.Convert.ToInt32(temp);
            }
            catch
            {
                return defaultValue;
            }
        }

        private DateTime GetItemToDateTime(string key, DateTime defaultValue = default)
        {
            if (_settings[key] == null)
                return defaultValue;

            try
            {
                string temp = _settings[key].Value.Trim();
                return System.Convert.ToDateTime(temp);
            }
            catch
            {
                return defaultValue;
            }
        }

        private void SetItem(string key, string value)
        {
            if (_settings[key] == null)
                _settings.Add(key, value);
            else
                _settings[key].Value = value;
        }
    }
}


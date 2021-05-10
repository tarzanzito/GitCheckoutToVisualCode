
using System;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Candal.Configuration
{

    [Serializable]
    [XmlRoot("App")]
    public class AppConfigRootXml : AppConfigData
    {
    }

    public class AppConfigXml : IAppConfig
    {
        private readonly string _fullExeFileName;
        private readonly string _fullConfigFileName;
        private readonly string _appConfigName;
        private AppConfigRootXml _appConfigRootXml;

        public AppConfigXml()
        {
            _fullExeFileName = Assembly.GetEntryAssembly().Location;
            _fullConfigFileName = $"{_fullExeFileName}.xml";
            _appConfigName = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
        }

        public AppConfigData Data
        {
            get { return _appConfigRootXml as AppConfigData; }
        }

        private void Create()
        {
            _appConfigRootXml = new AppConfigRootXml();

        }

        public void Load()
        {
            try
            {
                if (!File.Exists(_fullConfigFileName))
                    throw new FileNotFoundException($"File [{_appConfigName}] not found.");

                using (StreamReader str = new StreamReader(_fullConfigFileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AppConfigRootXml));
                    _appConfigRootXml = (AppConfigRootXml)serializer.Deserialize(str);
                    str.Close();
                }

                _appConfigRootXml.Decrypt();
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
                if (_appConfigRootXml == null)
                    throw new FileNotFoundException($"File [{_appConfigName}] not found.");

                _appConfigRootXml.Encrypt();

                string xmlText = "";
                XmlSerializer xmlSerializer = new XmlSerializer(_appConfigRootXml.GetType());

                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                    {
                        xmlSerializer.Serialize(xmlWriter, _appConfigRootXml);
                        xmlText = stringWriter.ToString();
                    }
                }

                using (StreamWriter streamWriter = File.CreateText(_fullConfigFileName))
                {
                    streamWriter.WriteLine(xmlText);
                }

            }
            catch (Exception ex)
            {
                _appConfigRootXml = new AppConfigRootXml(); //rever call cretate
                //todo: log
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candal.Configuration
{
    public enum ConfigExtention { yml, json, xml, config }

    public class ConfigFactory
    {
        private ConfigFactory()
        {
        }

        public static IAppConfig Choose()
        {
            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
            string fileName;

            foreach (ConfigExtention item in Enum.GetValues(typeof(ConfigExtention)))
            {
                fileName = $"{location}.{item}";
                if (!System.IO.File.Exists(fileName))
                    continue;

                switch(item)
                {
                    case ConfigExtention.yml:
                        return new AppConfigYaml();
                    case ConfigExtention.json:
                        return new AppConfigJson();
                    case ConfigExtention.xml:
                        return new AppConfigXml();
                    case ConfigExtention.config:
                        return new AppConfig();
                }
            }

            return new AppConfig();

            //string fileName = $"{location}.yml";
            //if (System.IO.File.Exists(fileName))
            //    return new AppConfigYaml();

            //fileName = $"{location}.json";
            //if (System.IO.File.Exists(fileName))
            //    return new AppConfigJson();

            //fileName = $"{location}.xml";
            //if (System.IO.File.Exists(fileName))
            //    return new AppConfigXml();

            //return new AppConfig();
        }

        public static void Create(ConfigExtention extention)
        {
            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
            string fileName;
            
            foreach (ConfigExtention item in Enum.GetValues(typeof(ConfigExtention)))
            {
                fileName = $"{location}.{item}";
                if (System.IO.File.Exists(fileName))
                    System.IO.File.Delete(fileName);
            }

            fileName = $"{location}.{extention}";
            StreamWriter streamWriter = System.IO.File.CreateText(fileName);
            streamWriter.Close();
        }
    }

}


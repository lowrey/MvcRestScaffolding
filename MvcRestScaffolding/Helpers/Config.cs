using System;
using System.IO;
using log4net;
using Newtonsoft.Json;
using MvcRestScaffolding.Models;

namespace MvcRestScaffolding.Helpers
{
    //Config is not used in the scaffolding but included as it is common once actual production code is used
    public class Config
    {
        private const string CONFIG_FILE = "ServerConfig.json";
        private string configPath;
        ILog log;
        public Config()
        {
            configPath = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), CONFIG_FILE);
            log = LogManager.GetLogger(this.GetType());
        }

        public ConfigModel GetConfig()
        {
            ConfigModel config = null;
            try
            {
                StreamReader re = new StreamReader(configPath);
                string json = re.ReadToEnd();
                re.Close();
                config = JsonConvert.DeserializeObject<ConfigModel>(json);
            }
            catch (Exception e)
            {
                log.Debug("Unable to get config: ", e);
            }
            return config;
        }

        public void WriteConfig(ConfigModel c)
        {
            using (StreamWriter file = new System.IO.StreamWriter(configPath))
            {
                string cStr = JsonConvert.SerializeObject(c, Formatting.Indented);
                file.Write(cStr);
            }
        }
    }
}
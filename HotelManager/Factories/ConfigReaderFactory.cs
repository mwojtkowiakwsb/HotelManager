using HotelManager.ConfigReader;
using HotelManager.Enums;

namespace HotelManager.Factories
{
    public class ConfigReaderFactory
    {
        public ConfigReaderFactory() 
        { 
        }

        public IConfigReader GetConfigReader(ConfigReaderEnum configReaderEnum)
        {
            return configReaderEnum switch
            {
                ConfigReaderEnum.JSON => new JsonConfigReader(),
                ConfigReaderEnum.XML => new XmlConfigReader(),  
                _ => throw new NotImplementedException($"Config reader {configReaderEnum} is not implemented")
            };;
        }
    }
}

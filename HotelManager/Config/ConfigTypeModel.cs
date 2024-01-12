

using System.Text.Json.Serialization;

namespace HotelManager.Config
{
    public class ConfigTypeModel
    {
        [JsonPropertyName("configType")]
        public string ConfigType { get; set; } = null!;
    }
}

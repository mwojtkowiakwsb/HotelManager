using HotelManager.Model;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace HotelManager.Config
{
    [XmlRoot("Root")]
    public class ConfigModel
    {
        [JsonPropertyName("hotelName")]
        [XmlElement("hotelName")]
        public string HotelName { get; set; } = null!;
        [JsonPropertyName("hotelColor")]
        [XmlElement("hotelColor")]
        public string HotelNameColorString { get; set; } = null!;

        [JsonIgnore]
        public ConsoleColor HotelNameColor => Enum.Parse<ConsoleColor>(HotelNameColorString);

        [JsonPropertyName("rooms")]
        [XmlArray("rooms")]
        [XmlArrayItem("room", typeof(RoomInfo))]
        public List<RoomInfo> Rooms { get; set; } = new List<RoomInfo>();

        [JsonPropertyName("users")]
        [XmlArray("users")]
        [XmlArrayItem("user", typeof(User))]    
        public List<User> Users { get; set; } = new List<User>();

    }
}

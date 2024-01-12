using HotelManager.Enums;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace HotelManager.Model
{
    public class RoomInfo
    {
        public int Id { get; set; }
        [JsonPropertyName("isAvailable")]
        [XmlElement("isAvailable")]
        public bool IsAvailable { get; set; }
        [JsonPropertyName("price")]
        [XmlElement("price")]
        public double Price { get; set; }
        [JsonPropertyName("description")]
        [XmlElement("description")]
        public string Description { get; set; } = null!;
        [JsonPropertyName("type")]
        [XmlElement("type")]
        public string Type { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [JsonIgnore]
        public RoomType RoomType => Enum.Parse<RoomType>(Type);
    };
}

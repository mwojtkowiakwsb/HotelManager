using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace HotelManager.Model
{
    public class User
    {
        public int Id { get; set; } 
        [JsonPropertyName("email")]
        [XmlElement("email")]
        public string Email { get; set; } = null!;
        [JsonPropertyName("password")]
        [XmlElement("password")]
        public string Password { get; set; } = null!;
        [JsonPropertyName("balance")]
        [XmlElement("balance")]
        public double Balance { get; set; }
        [JsonPropertyName("isAdmin")]
        [XmlElement("isAdmin")]
        public bool isAdmin { get; set; }
    }
}

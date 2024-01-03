
using HotelManager.Enums;

namespace HotelManager.Constants.Dictionaries
{
    public static class RoomDictionary
    {
        public static IDictionary<RoomType, string> RoomTypeDict = new Dictionary<RoomType, string>()
        {
            { RoomType.Common, "Zwykły" },
            { RoomType.Luxurious, "Luksusowy" },
        };
    }
}

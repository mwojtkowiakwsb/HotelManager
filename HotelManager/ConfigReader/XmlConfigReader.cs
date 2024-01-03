using HotelManager.Model;
using HotelManager.Rooms;
using System.Xml.Serialization;

namespace HotelManager.ConfigReader
{
    public class XmlConfigReader : IConfigReader
    {
        public T ReadConfig<T>(string path)
        {
            var content = File.ReadAllText(path);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader stringReader = new StringReader(content))
            {
                return (T)xmlSerializer.Deserialize(stringReader)!;
            }
        }

        public void SaveRoomsState(IList<IRoom> bookedRooms, IList<RoomInfo> actualRooms)
        {
            throw new NotImplementedException();
        }
    }
}

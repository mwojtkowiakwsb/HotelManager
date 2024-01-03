using HotelManager.Enums;
using HotelManager.Model;

namespace HotelManager.Rooms
{
    public abstract class BaseRoom : IRoom
    {
        public bool IsAvailable { get; private set; }
        public int RoomId { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public RoomType RoomType { get; private set; }
        public List<Type> UsedDecorators { get; private set; }

        public BaseRoom(RoomInfo hotelInfo)
        {
            IsAvailable = hotelInfo.IsAvailable;
            RoomId = hotelInfo.Id;
            Description = hotelInfo.Description;
            Price = hotelInfo.Price;
            RoomType = hotelInfo.RoomType;
            UsedDecorators = new List<Type> { };
        }

        public void SetAvailability(bool availability)
        {
            IsAvailable = availability;
        }
    }
}

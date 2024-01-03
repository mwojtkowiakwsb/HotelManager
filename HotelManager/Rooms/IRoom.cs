using HotelManager.Enums;

namespace HotelManager.Rooms
{
    public interface IRoom
    {
        List<Type> UsedDecorators { get; }
        bool IsAvailable { get; }
        int RoomId { get; }
        string Description { get; }
        double Price { get; }
        RoomType RoomType { get; }
        public void SetAvailability(bool isAvailable);
    }
}

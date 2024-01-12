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
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        RoomType RoomType { get; }
        public void SetAvailability(bool isAvailable);
        public void SetStartDate(DateTime startDate);
        public void SetEndDate(DateTime endDate);
    }
}

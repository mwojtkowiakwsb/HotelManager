

namespace HotelManager.Rooms
{
    public abstract class Room
    {
        protected bool Available { get; set; }
        protected int RoomId { get; set; }
        protected string Description { get; set; } = null!;
        protected double Price { get; set; }
    }
}

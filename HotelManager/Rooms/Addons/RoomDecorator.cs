

using HotelManager.Enums;

namespace HotelManager.Rooms.Addons
{
    public abstract class RoomDecorator : IRoom
    {
        protected IRoom _decoratedRoom;

        public RoomDecorator(IRoom decoratedRoom)
        {
            _decoratedRoom = decoratedRoom;
        }

        public virtual bool IsAvailable => _decoratedRoom.IsAvailable;
        public virtual int RoomId => _decoratedRoom.RoomId;
        public virtual string Description => _decoratedRoom.Description;
        public virtual double Price => _decoratedRoom.Price;
        public virtual RoomType RoomType => _decoratedRoom.RoomType;
        public virtual List<Type> UsedDecorators => _decoratedRoom.UsedDecorators;

        public virtual void SetAvailability(bool isAvailable)
        {
            _decoratedRoom.SetAvailability(isAvailable);
        }
    }
}

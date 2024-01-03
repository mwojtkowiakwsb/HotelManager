

namespace HotelManager.Rooms.Addons
{
    public class SwimmingPoolAccess : RoomDecorator
    {
        public SwimmingPoolAccess(IRoom decoratedRoom) : base(decoratedRoom)
        {
            if (decoratedRoom.UsedDecorators.Contains(typeof(SwimmingPoolAccess)))
            {
                throw new ArgumentException("Dostep do basenu jest już dodany do pokoju");
            };
            _decoratedRoom = decoratedRoom;
            _decoratedRoom.UsedDecorators.Add(typeof(SwimmingPoolAccess));
        }

        public override string Description => _decoratedRoom.Description + ", z dostępem do Basenu";

        public override double Price => _decoratedRoom.Price + 30.00;

        public override List<Type> UsedDecorators => _decoratedRoom.UsedDecorators;
    }
}

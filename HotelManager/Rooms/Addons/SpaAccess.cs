

namespace HotelManager.Rooms.Addons
{
    public class SpaAccess : RoomDecorator
    {
        public SpaAccess(IRoom decoratedRoom) : base(decoratedRoom)
        {
            if (decoratedRoom.UsedDecorators.Contains(typeof(SpaAccess)))
            {
                throw new ArgumentException("SPA jest już dodane do pokoju");
            };
            _decoratedRoom = decoratedRoom;
            _decoratedRoom.UsedDecorators.Add(typeof(SpaAccess));
        }

        public override string Description => _decoratedRoom.Description + ", z dostępem do SPA";

        public override double Price => _decoratedRoom.Price + 20.00;

        public override List<Type> UsedDecorators => _decoratedRoom.UsedDecorators; 
    }
}

using HotelManager.Payment;
using HotelManager.Rooms;
using HotelManager.Rooms.Addons;

namespace HotelManager.Factories
{
    public class RoomDecoratorFactory
    {
        public IRoom GetRoomDecorator(int decoratorNumber, IRoom room)
        {
            return decoratorNumber switch
            {
                1 => new SpaAccess(room),
                2 => new SwimmingPoolAccess(room),
                _ => throw new ArgumentException($"Opcja o numerze {3} nie jest dostępna.")
            };
        }
    }
}

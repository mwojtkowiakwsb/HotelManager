using HotelManager.Model;
using HotelManager.Rooms;

namespace HotelManager.Payment
{
    public interface IPaymentStrategy
    {
        public bool CollectPaymentData();
        public bool Pay(User user, IList<IRoom> rooms);
    }
}

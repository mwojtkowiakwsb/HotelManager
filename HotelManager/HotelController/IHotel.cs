using HotelManager.Enums;
using HotelManager.Model;
using HotelManager.Payment;
using HotelManager.Rooms;

namespace HotelManager.HotelController
{
    public interface IHotel
    {
        public bool BookRoom(int bookId);

        public void ListRoomsWithGivenType(RoomType roomType);

        public void CancelBookedRoom(int roomId);

        public void ShowBookedRooms(ShowSingleRoomOption option);

        public void SetPayment(IPaymentStrategy paymentStrategy);

        public bool ProceedWithPayment(User user);

    }
}

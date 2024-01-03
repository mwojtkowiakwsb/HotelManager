using HotelManager.Constants.Dictionaries;
using HotelManager.Enums;
using HotelManager.Model;
using HotelManager.Payment;
using HotelManager.Repository;
using HotelManager.Rooms;

namespace HotelManager.HotelController
{
    public class Hotel : IHotel
    {
        private readonly IRoomRepository roomRepository;
        private readonly List<IRoom> _bookedRooms = new List<IRoom>();
        private IPaymentStrategy? _paymentStrategy;

        public Hotel(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public IList<IRoom> BookedRooms => _bookedRooms;

        public double BookedRoomsPrice => BookedRooms.Select(room => room.Price).Sum();

        public string BookedRoomsTextList => string.Join(", ", BookedRooms.Select(room => room.RoomId).ToList());

        public IRoom GetRoomToBook(int roomId)
        {
            var roomFromDb = roomRepository.GetRoomById(roomId);
            var roomDto = roomFromDb.RoomType == RoomType.Common ? new CommonRoom(roomFromDb) : (IRoom)new LuxoriousRoom(roomFromDb);
            return roomDto;
        }

        public void ListRoomsWithGivenType(RoomType roomType)
        {
            Console.WriteLine("Wyświetlanie POKOI");
            roomRepository.GetRoomsWithType(roomType).ToList().ForEach(room =>
                {
                    ShowSingleRoom(room);
                });
        }

        private bool CheckIfRoomIsAvailable(IRoom room)
        {
            if (room.IsAvailable)
            {
                Console.WriteLine("Pokój jest dostępny!");
            }
            else
            {
                Console.WriteLine("Pokój nie jest dostępny.");
            }
            return room.IsAvailable;
        }

        public IRoom GetRoom(int roomId, RoomState roomState)
        {
            return roomState switch
            {
                RoomState.BookedRoom => BookedRooms.ToList().Find(room => room.RoomId == roomId) ?? throw new ArgumentNullException($"Pokój o numerze {roomId} nie istnieje"),
                _ => throw new ArgumentException($"Podaj właściwy stan pokoju do przeszukania. Taki stan {roomState} nie jest obsługiwany")
            };

        }

        public IRoom GetRoom(int roomId, RoomType roomType, RoomState roomState)
        {
            var room = GetRoom(roomId, roomState);
            if (room == null)
            {
                throw new ArgumentException($"Pokój o numerze {roomId} nie istnieje");
            }
            var isLuxurious = roomType == RoomType.Luxurious;
            if (room.RoomType != roomType)
            {
                throw new ArgumentException($"Pamiętaj, wybrany pokój musi być pokojem {(isLuxurious ? "luksusowym" : "zwykłym")} a nie {(isLuxurious ? "zwykłym" : "luksusowym")}");
            }
            return room;
        }

        public void UpdateBookedRoom(IRoom updatedRoom)
        {
            var index = BookedRooms.ToList().FindIndex(r => r.RoomId == updatedRoom.RoomId);

            if (index != -1)
            {
                Console.WriteLine("Cena");
                Console.WriteLine(updatedRoom.Price);
                BookedRooms[index] = updatedRoom;
                Console.WriteLine(BookedRooms[index].Price);
            }
            else
            {
                throw new InvalidOperationException("Pokój nie został znaleziony");
            }
        }

        public bool BookRoom(int roomId)
        {
            Console.WriteLine($"Rezerwowanie pokoju o numerze {roomId}");
            var roomToBook = GetRoomToBook(roomId); 
            var isRoomAvailable = CheckIfRoomIsAvailable(roomToBook);
            if (isRoomAvailable)
            {
                roomToBook.SetAvailability(false);
                Console.WriteLine($"Pokój o numerze {roomToBook.RoomId} został zarezerwowany!");
                _bookedRooms.Add(roomToBook);
                return true;
            }
            Console.WriteLine($"Pokój o numerze {roomToBook.RoomId} nie został zarezerwowany! Proszę wybierz inny.");
            return false;
        }

        public void ShowBookedRooms(ShowSingleRoomOption option = ShowSingleRoomOption.ShowWithAvailability)
        {
            Console.WriteLine("WYŚWIETLANIE ZAREZERWOWANYCH POKOI");
            if (BookedRooms.Count == 0) Console.WriteLine("Brak zarezerwowanych pokoi");
            else BookedRooms.ToList().ForEach((room) => ShowSingleRoom(room, option));
        }

        public void SetPayment(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public bool ProceedWithPayment(User user)
        {
            var collectedSuccessfully = _paymentStrategy.CollectPaymentData();
            if (collectedSuccessfully)
            {
                _paymentStrategy.Pay(user, BookedRooms);
                Console.WriteLine($"Zarezerwowane pokoje {BookedRoomsTextList}");
                return true;
            }
            return false;
        }

        private void ShowSingleRoom(IRoom room, ShowSingleRoomOption option = ShowSingleRoomOption.ShowWithAvailability)
        {
            ShowSingleRoom(new RoomInfo() { Id = room.RoomId, Description = room.Description, IsAvailable = room.IsAvailable, Type = room.RoomType.ToString(), Price = room.Price }, option);
        }

        private void ShowSingleRoom(RoomInfo room, ShowSingleRoomOption option = ShowSingleRoomOption.ShowWithAvailability)
        {
            Console.WriteLine($">>>>>>>POKÓJ {room.Id}<<<<<<<<<");
            Console.WriteLine(room.Description);
            if (option == ShowSingleRoomOption.ShowWithAvailability)
            {
                Console.WriteLine($"Dostępny: {(room.IsAvailable ? "TAK" : "NIE")}");
            }
            Console.WriteLine($"Cena: {room.Price}");
            Console.WriteLine($"Typ: {RoomDictionary.RoomTypeDict[room.RoomType]}");
            Console.WriteLine($">>>>>>>><<<<<<<<<");
        }

        public void CancelBookedRoom(int roomId)
        {
            var room = GetRoom(roomId, RoomState.BookedRoom);
            room.SetAvailability(true);
            BookedRooms.Remove(room);
            Console.WriteLine($"Udało się anulować rezerwacje pokoju {roomId}");
        }
    }
}

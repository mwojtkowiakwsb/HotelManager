using HotelManager.Enums;
using HotelManager.HotelController;
using HotelManager.Model;
using HotelManager.Payment;
using HotelManager.Repository;
using HotelManager.Rooms;
using HotelManager.Utils;

namespace HotelManager.Factories
{
    public class PanelFactory
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;

        public PanelFactory(IRoomRepository roomRepository, IUserRepository userRepository) 
        {
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }

        public void GetPanel(Hotel hotel, User loggedInUser)
        {
            int option = 1000;
            if (loggedInUser.isAdmin)
            {
                while (option != 3)
                {
                    try
                    {
                        Console.WriteLine("<<<<<< ZNAJDUJESZ SIĘ W PANELU ADMINISTRACYJNYM >>>>>>");
                        Console.WriteLine("Wybierz opcję 1-2: ");
                        Console.WriteLine("1. Dodaj pokoje");
                        Console.WriteLine("2. Usuń pokój");
                        Console.WriteLine("3. Wyjdź");
                        option = ConsoleUtils.ReadInt();
                        switch (option)
                        {
                            case 1:
                                List<RoomInfo> rooms = new List<RoomInfo>();
                                int addNextRooms = 1000;
                                while (addNextRooms != 2)
                                {
                                    Console.WriteLine("Podaj Opis pokoju");
                                    var roomDescription = ConsoleUtils.ReadString();
                                    Console.WriteLine("Podaj cenę pokoju");
                                    var roomPrice = ConsoleUtils.ReadInt();
                                    var roomModel = new RoomInfo() { Description = roomDescription, Price = roomPrice, IsAvailable = true };
                                    Console.WriteLine("Wybierz typ pokoju 1. Zwykly 2. Luksusowy");
                                    var roomType = ConsoleUtils.ReadInt();
                                    switch (roomType)
                                    {
                                        case 1:
                                            roomModel.Type = RoomType.Common.ToString();
                                            break;
                                        case 2:
                                            roomModel.Type = RoomType.Luxurious.ToString();
                                            break;
                                    }
                                    rooms.Add(roomModel);   
                                    Console.WriteLine("Czy chcesz dodać kolejne pokoje? 1.Tak 2.Nie");
                                    addNextRooms = ConsoleUtils.ReadInt();
                                }
                                _roomRepository.AddRange(rooms);
                                break;
                            case 2:
                                hotel.ListRoomsWithGivenType(RoomType.Common);
                                hotel.ListRoomsWithGivenType(RoomType.Luxurious);
                                Console.WriteLine("Podaj ID pokoju do usunięcia");
                                var roomToDelete = ConsoleUtils.ReadInt();
                                _roomRepository.Remove(roomToDelete);
                                Console.WriteLine($"Pomyślnie usunięto pokój o ID {roomToDelete}");
                                break;
                            case 3:
                                option = 3;
                                break;
                            default: throw new ArgumentException("Proszę wybrać opcję od 1-3");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Wystąpił błąd. Spróbuj ponownie.");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Naciśnij dowolny przycisk by wrócić do menu");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                while (option != 6)
                {
                    try
                    {
                        Console.WriteLine("Wybierz opcję 1-5: ");
                        Console.WriteLine("1. Wyświetl pokoje");
                        Console.WriteLine("2. Zarezerwuj pokój");
                        Console.WriteLine("3. Skonfiguruj pokój");
                        Console.WriteLine("4. Cofnij rezerwację");
                        Console.WriteLine("5. Przejdź do płatności");
                        Console.WriteLine("6. Wyjdź");
                        Console.WriteLine($"Aktualnie zarezerwowane pokoje [{hotel.BookedRoomsTextList}] [KOSZT: {hotel.BookedRoomsPrice}]");
                        Console.WriteLine($"Twój stan konta {loggedInUser.Balance}");
                        option = ConsoleUtils.ReadInt();
                        switch (option)
                        {
                            case 1:
                                Console.WriteLine("Jakie pokoje chcesz wyświetlić?");
                                Console.WriteLine("1. Zwykłe");
                                Console.WriteLine("2. Luksusowe");
                                Console.WriteLine("3. Wróć");
                                int listOption = ConsoleUtils.ReadInt();
                                switch (listOption)
                                {
                                    case 1: hotel.ListRoomsWithGivenType(RoomType.Common); break;
                                    case 2: hotel.ListRoomsWithGivenType(RoomType.Luxurious); break;
                                    default: throw new ArgumentException($"Proszę wybierz poprawną opcję. Opcja o numerze {listOption} nie istnieje");
                                }
                                break;
                            case 2:
                                bool isBooked = false;
                                bool shouldLeave = false;
                                while (!isBooked && !shouldLeave)
                                {
                                    Console.WriteLine(">>>>>>>> Rozpoczynasz rezerwację pokoju >>>>>>>>");
                                    Console.WriteLine("1. Wybierz pokój\n2. Wróć");
                                    var bookingOption = ConsoleUtils.ReadInt();
                                    switch (bookingOption)
                                    {
                                        case 1:
                                            Console.WriteLine("Podaj numer pokoju");
                                            var roomId = ConsoleUtils.ReadInt();
                                            Console.WriteLine("Podaj date przyjazdu");
                                            var startDate = ConsoleUtils.ReadDate();
                                            Console.WriteLine("Podaj date odjazdu");
                                            var endDate = ConsoleUtils.ReadDate();
                                            isBooked = hotel.BookRoom(roomId, startDate, endDate);
                                            break;
                                        case 2:
                                            Console.WriteLine("Wracasz do menu głównego");
                                            shouldLeave = true;
                                            break;
                                    }
                                }
                                break;
                            case 3:
                                Console.WriteLine(">>>>>>>> Rozpoczynasz konfigurację pokoju >>>>>>>>");
                                Console.WriteLine($"Twoje zarezerwowane pokoje");
                                hotel.ShowBookedRooms();
                                if (hotel.BookedRooms.Count == 0) throw new ArgumentException("Brak wybranych pokoi, nie jesteś w stanie skonfigurować żadnego pokoju");
                                Console.WriteLine("Wybierz pokój, podaj jego numer. Pamiętaj, nie możesz konfigurować pokoi luksusowych, mają wszystkie dodatki.");
                                var roomToModifyId = ConsoleUtils.ReadInt();
                                var roomToModify = hotel.GetRoom(roomToModifyId, RoomType.Common, RoomState.BookedRoom);
                                Console.WriteLine("POKÓJ DO MODYFIKACJI");
                                Console.WriteLine(roomToModify.Price);
                                Console.WriteLine(roomToModify.UsedDecorators.Count);
                                Console.WriteLine("1. Dostep do SPA (+20) 2. Dostęp do basenu (+30)");
                                var selectedExtension = ConsoleUtils.ReadInt();
                                var roomDecorator = new RoomDecoratorFactory().GetRoomDecorator(selectedExtension, roomToModify);
                                hotel.UpdateBookedRoom(roomDecorator);
                                break;
                            case 4:
                                Console.WriteLine("Jesteś w procesie anulowania rezerwacji pokoju");
                                Console.WriteLine($"Twoje zarezerwowane pokoje: [{string.Join("", hotel.BookedRooms.Select(room => room.RoomId).ToList())}]");
                                hotel.ShowBookedRooms(ShowSingleRoomOption.ShowWithoutAvailability);
                                Console.WriteLine("Z jakiego pokoju, chcesz zrezygnować? Podaj jego numer");
                                var selectedRoomForCancellation = ConsoleUtils.ReadInt();
                                hotel.CancelBookedRoom(selectedRoomForCancellation);
                                break;
                            case 5:
                                Console.WriteLine("Przechodzisz do płatności");
                                Console.WriteLine("Zarezerwowane pokoje: ");
                                hotel.ShowBookedRooms(ShowSingleRoomOption.ShowWithoutAvailability);
                                Console.WriteLine($"Całkowity koszt: {hotel.BookedRooms.Select(room => room.Price).Sum()}");
                                Console.WriteLine("Wybierz metodę płatności. \n1.Przelew bankowy \n2.Blik");
                                var paymentOption = ConsoleUtils.ReadInt();
                                IPaymentStrategy strategy = new PaymentStrategyFactory().GetPaymentStrategy(paymentOption);
                                hotel.SetPayment(strategy);
                                var isPaymentSuccessful = hotel.ProceedWithPayment(loggedInUser);
                                if (isPaymentSuccessful)
                                {
                                    _roomRepository.UpdateBasedOnList(hotel.BookedRooms);
                                    _userRepository.Update(loggedInUser);
                                    option = 6;
                                }
                                break;
                            case 6:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Naciśnij dowolny przycisk by wrócić do menu");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}

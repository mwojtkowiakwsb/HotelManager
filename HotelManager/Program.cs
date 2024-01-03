using HotelManager.Authorization;
using HotelManager.Config;
using HotelManager.Data;
using HotelManager.Enums;
using HotelManager.Factories;
using HotelManager.HotelController;
using HotelManager.Model;
using HotelManager.Repository;

namespace HotelManager
{
    public class Program
    {
        private static readonly ConfigReaderEnum ConfigEnum = ConfigReaderEnum.XML;
        private static readonly bool InitialLoad = true;
        private static string GetConfigPath(string extension) => Path.Join(Directory.GetCurrentDirectory(), $"Config\\config.{extension}");

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            var dbContext = new HotelContext();
            var roomRepository = new RoomRepository(dbContext); 
            var userRepository = new UserRepository(dbContext); 
            var hotel = new Hotel(roomRepository);

            Console.WriteLine(">>>>>>>> WCZYTYWANIE KONFIGURACJI SYSTEMU >>>>>>>>");
            var configReader = new ConfigReaderFactory().GetConfigReader(ConfigEnum);
            var configPath = GetConfigPath(ConfigEnum.ToString().ToLower());
            var config = configReader.ReadConfig<ConfigModel>(configPath);
            if (config.Rooms == null)
            {
                throw new ArgumentNullException("Nie dodano pokoi w pliku konfiguracyjnym");
            }

            if (InitialLoad)
            {
                roomRepository.AddRangeUnique(config.Rooms);
                userRepository.AddRangeUnique(config.Users);
            }

            Console.ForegroundColor = config.HotelNameColor;
            Console.WriteLine($">>>>>>>> WITAJ W SYSTEMIE REZERWACJI POKOJÓW W HOTELU {config.HotelName} >>>>>>>>");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Proszę się zalogować lub utworzyć konto");
            User? loggedInUser = null;
            var authorizationService = new AuthorizationService(userRepository);
            while (loggedInUser == null)
            {
                try
                {
                    loggedInUser = authorizationService.HandleAuthorization();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                }
            }
            new PanelFactory(roomRepository, userRepository).GetPanel(hotel, loggedInUser);
        }
    }
}
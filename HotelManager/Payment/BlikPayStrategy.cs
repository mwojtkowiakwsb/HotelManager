using HotelManager.Model;
using HotelManager.Rooms;
using HotelManager.Utils;

namespace HotelManager.Payment
{
    public class BlikPayStrategy : IPaymentStrategy
    {
        private static double Commission = 0.5;
        public bool CollectPaymentData()
        {
            Console.WriteLine("Zbieranie danych do płatności BLIK");
            Console.WriteLine("Proszę podaj kod BLIK");
            var blikCode = ConsoleUtils.ReadString();
            return ValidateCode(blikCode);
        }

        public bool Pay(User user, IList<IRoom> rooms)
        {
            Console.WriteLine("Płacę...");
            var price = CalculatePrice(rooms);
            Console.WriteLine($"Cena z prowizją blika ({Commission}) to {price}");
            if (user.Balance < price)
            {
                throw new ArgumentException($"Masz nie wystarczająco środków ({user.Balance}), potrzeba {price - user.Balance} więcej");
            }
            user.Balance -= price;
            Console.WriteLine("Płatność się powiodła gratuluję! Wybrane pokoje zostały zarezerwowane");
            return true;
        }

        private bool ValidateCode(string code)
        {
            if (code.Length != 6)
            {
                Console.WriteLine("Kod powinien mieć długość 6");
                return false;
            }

            var isNumeric = int.TryParse(code, out var numericCode);
            if (!isNumeric)
            {
                Console.WriteLine("Kod powinien składać się wyłącznie z cyfr");
                return false;
            }
            Console.WriteLine("Kod jest poprawny!");
            return true;
        }

        private double CalculatePrice(IList<IRoom> rooms)
        {
            return rooms.Sum(room => room.Price) + Commission;
        }
    }
}

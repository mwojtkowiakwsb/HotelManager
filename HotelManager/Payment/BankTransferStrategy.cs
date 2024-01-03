using HotelManager.Constants;
using HotelManager.Model;
using HotelManager.Rooms;
using HotelManager.Utils;

namespace HotelManager.Payment
{
    public class BankTransferStrategy : IPaymentStrategy
    {
        private static double Commission = 5;
        public bool CollectPaymentData()
        {
            Console.WriteLine("Zbieranie danych do przelewu bankowego.");
            Console.WriteLine("Proszę podaj imię");
            var name = ConsoleUtils.ReadString();
            Console.WriteLine("Proszę podaj nazwisko");
            var lastName = ConsoleUtils.ReadString();
            Console.WriteLine("Proszę podaj kod bezpieczeństwa CVV (3 znaki)");
            var CCVCode = ConsoleUtils.ReadString();
            Console.WriteLine("Proszę podaj numer karty (16 cyfr)");
            var cardNumber = ConsoleUtils.ReadString();
            var cardInfo = new BankTransferInfo()
            {
                Name = name,
                LastName = lastName,
                CCV = CCVCode,
                CardNumber = cardNumber
            };
            Console.WriteLine(">>> WALIDACJA PODANYCH INFORMACJI <<<");
            return ValidateBankTransfer(cardInfo);
        }

        public bool Pay(User user, IList<IRoom> rooms)
        {
            Console.WriteLine("Płacę...");
            var price = CalculatePrice(rooms);
            Console.WriteLine($"Cena z prowizją banku ({Commission}) to {price}");
            if (user.Balance < price)
            {
                throw new ArgumentException($"Masz nie wystarczająco środków ({user.Balance}), potrzeba {price - user.Balance} więcej");
            }
            user.Balance -= price;
            Console.WriteLine("Płatność się powiodła gratuluję! Wybrane pokoje zostały zarezerwowane");
            return true;
        }

        private bool ValidateBankTransfer(BankTransferInfo bankTransferInfo)
        {
            if (!RegexUtils.IsMatch(bankTransferInfo.Name, RegexPattern.OnlyLetters) || !RegexUtils.IsMatch(bankTransferInfo.LastName, RegexPattern.OnlyLetters))
            {
                throw new FormatException("Imię i nazwisko musi zawierać wyłącznie litery");
            };
            if (!RegexUtils.IsMatch(bankTransferInfo.Name, RegexPattern.OnlyLetters) || !RegexUtils.IsMatch(bankTransferInfo.LastName, RegexPattern.OnlyLetters))
            {
                throw new FormatException("Imię i nazwisko musi zawierać wyłącznie litery");
            };
            if (!int.TryParse(bankTransferInfo.CCV, out int result))
            {
                throw new FormatException("Kod CCV musi zawierać wyłącznie cyfry.");
            }
            if (bankTransferInfo.CCV.Length != 3)
            {
                throw new FormatException("Kod CCV musi powinien zawierać 3 cyfry");
            }
            if (!int.TryParse(bankTransferInfo.CardNumber, out int cardNumberResult))
            {
                throw new FormatException("Numer karty musi zawierać wyłącznie cyfry.");
            }
            if (bankTransferInfo.CardNumber.Length != 16)
            {
                throw new FormatException("Numer karty powinien mieć długość 16");
            }
            return true;
        }

        private double CalculatePrice(IList<IRoom> rooms)
        {
            return rooms.Sum(room => room.Price) + Commission;
        }
    }
}

using HotelManager.Model;
using HotelManager.Repository;
using HotelManager.Utils;

namespace HotelManager.Authorization
{
    public class AuthorizationService
    {
        private readonly UserRepository _userRepository;

        public AuthorizationService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User HandleAuthorization()
        {
            Console.WriteLine("1. Zaloguj się\n2. Zarejestruj się");
            var registerOption = ConsoleUtils.ReadInt();

            switch (registerOption)
            {
                case 1:
                    Console.WriteLine("EMAIL: ");
                    var email = ConsoleUtils.ReadString();
                    Console.WriteLine("HASŁO: ");
                    var password = ConsoleUtils.ReadString();
                    return Login(email, password);
                case 2:
                    Console.WriteLine("EMAIL: ");
                    var newUserEmail = ConsoleUtils.ReadString();
                    Console.WriteLine("HASŁO: ");
                    var newPassword = ConsoleUtils.ReadString();
                    return CreateUser(newUserEmail, newPassword);
                default: throw new ArgumentException("Proszę wybrać opcję od 1-2");
            }
        }

        private User Login(string email, string password)
        {
            Validate(email, password);
            var loggedUser = _userRepository.GetByEmailAndPassword(email, password);
            if (loggedUser == null)
            {
                throw new ArgumentException($"Użytkownik o emailu {email} nie został znaleziony. Spróbuj ponownie");
            }
            Console.WriteLine("Logowanie powiodło się!");
            return loggedUser;
        }

        private User CreateUser(string email, string password)
        {
            Validate(email, password);
            var newUser = new User() { Email = email, Password = password, isAdmin = false, Balance = 500 };
            _userRepository.Add(newUser);
            Console.WriteLine("Rejestracja powiodła się! Początkowa kwota na koncie to 500");
            return newUser;
        }

        private void Validate(string email, string password)
        {
            if (email.Length == 0)
            {
                throw new ArgumentException("Email nie może być pusty");
            }

            if (password.Length == 0)
            {
                throw new ArgumentException("Hasło nie może być puste");
            }
        }
    }
}

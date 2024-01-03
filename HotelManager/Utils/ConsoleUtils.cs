

namespace HotelManager.Utils
{
    public static class ConsoleUtils
    {
        public static int ReadInt()
        {
            try
            {
                return int.Parse(Console.ReadLine());
            } catch (Exception e)
            {
               throw new FormatException("Musisz podać liczbę!");
            }
        }

        public static double ReadDouble()
        {
            try
            {
                return double.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                throw new FormatException("Musisz podać liczbę!");
            }
        }

        public static string ReadString()
        {
           var userInput = Console.ReadLine();
           if (userInput == null)
           {
                throw new FormatException("Musisz podać wartość, pole nie może być puste");
           }
           return userInput;
        }
    }
}

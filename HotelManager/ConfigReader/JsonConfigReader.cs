using System.Text.Json;

namespace HotelManager.ConfigReader
{
    public class JsonConfigReader : IConfigReader
    {
        public T ReadConfig<T>(string path)
        {
            var file = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(file) ?? throw new FileNotFoundException("File does not exist");
        }
    }
}

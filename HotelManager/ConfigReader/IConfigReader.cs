
namespace HotelManager.ConfigReader
{
    public interface IConfigReader
    {
        T ReadConfig<T>(string path);
    }
}

namespace Infrastructures.JsonConverts
{
    public interface IJsonConverter
    {
        T Deserialize<T>(string value);
        string Serialize<T>(T obj);
    }
}

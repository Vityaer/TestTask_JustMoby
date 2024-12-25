using Common;
using Newtonsoft.Json;
using System.IO;

namespace Utils.Texts
{
    public static class TextUtils
    {
        public static void Save<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data, ProjectConstants.Serialization.SerializerSettings);
            File.WriteAllText(GetConfigPath<T>(), json);
        }

        public static T Load<T>() where T : new()
        {
            var json = GetTextFromLocalStorage<T>();
            var result = JsonConvert.DeserializeObject<T>(json, ProjectConstants.Serialization.SerializerSettings);

            if (result == null)
            {
                result = new T();
            }

            return result;
        }

        private static string GetTextFromLocalStorage<T>()
        {
            var path = GetConfigPath<T>();
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            var text = File.ReadAllText(path);
            return text;
        }

        public static string GetConfigPath<T>()
        {
            var path = Path.Combine(ProjectConstants.Common.DictionariesPath, $"{typeof(T).Name}.json");
            return path;
        }
    }
}

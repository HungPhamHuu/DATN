using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos.Core;
using Newtonsoft.Json;
using System.Text;

namespace AppData.Session
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        private static void SetString(this ISession session, string key, string value)
        {
            session.Set(key, Encoding.UTF8.GetBytes(value));
        }

        private static string GetString(this ISession session, string key)
        {
            var valueBytes = session.Get(key);
            return valueBytes == null ? null : Encoding.UTF8.GetString(valueBytes);
        }

        private static byte[] Get(this ISession session, string key)
        {
            return session.TryGetValue(key, out var value) ? value : null;
        }
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}

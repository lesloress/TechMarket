using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace TechMarket.Infrastructure
{
    public static class SessionExtensions
    {
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static string GetCartId(this ISession session)
        {
            var cartId = session.GetString("CartId");
            session.SetString("CartId", cartId);
            return cartId;
        }
    }
}

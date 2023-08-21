using System.Text;
using System.Text.Json;
using Blazored.SessionStorage;

namespace AUA.ProjectName.WebUi.Utility
{
    public static class SessionStorageServiceExtension
    {
        public static async Task SaveItemEncryptedAsync<T>(this ISessionStorageService sessionStorageService,
            string key, T item)
        {
            var itemJson = JsonSerializer.Serialize(item);

            var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson);

            var itemBase64 = Convert.ToBase64String(itemJsonBytes);

            await sessionStorageService.SetItemAsync(key, itemBase64);
        }

        public static async Task<T> ReadItemEncryptedAsync<T>(this ISessionStorageService sessionStorageService,
            string key)
        {
            var itemBase64 = await sessionStorageService.GetItemAsync<string>(key);

            var itemJsonBytes = Convert.FromBase64String(itemBase64);

            var itemJson = Encoding.UTF8.GetString(itemJsonBytes);

            return JsonSerializer.Deserialize<T>(itemJson);
        }
    }
}

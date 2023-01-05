using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

public static class DistributedCacheExtension
{
    public static async Task<string> SetAsync(this IDistributedCache cache, string keyPrefix, CacheModel cacheModel)
    {
        var guid = keyPrefix+DateTime.Now.ToString("yyyyMMddHHmmssffffff") + Guid.NewGuid().ToString();
        //await cache.SetAsync(guid, JsonSerializer.SerializeToUtf8Bytes(cacheModel));
        await cache.SetAsync(guid, Encoding.Unicode.GetBytes(JsonSerializer.Serialize(cacheModel)));
        return guid;
    }
    public static async Task<string> SetStringAsync(this IDistributedCache cache, string keyPrefix, CacheModel cacheModel)
    {
        var guid = keyPrefix + DateTime.Now.ToString("yyyyMMddHHmmssffffff") + Guid.NewGuid().ToString();
        await cache.SetStringAsync(guid, JsonSerializer.Serialize(cacheModel));
        return guid;
    }
    public static async Task<CacheModel> GetByStringAsync(this IDistributedCache cache, string key)
    {
        var result= await cache.GetStringAsync(key,CancellationToken.None);
        return JsonSerializer.Deserialize<CacheModel>(result);
    }
    public static async Task<CacheModel> GetByByteAsync(this IDistributedCache cache, string key)
    {
        var result = await cache.GetAsync(key);
        return JsonSerializer.Deserialize<CacheModel>(result);
    }
}

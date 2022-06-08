using System.Text.Json;
using Business.API.Models;
using StackExchange.Redis;

namespace Business.API.Repository;

public class RedisRepository
{
    private readonly IConnectionMultiplexer _redis;

    public RedisRepository(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public Task CreateKurList(List<Kur>? kur)
    {
        var db = _redis.GetDatabase();
        foreach (var kur1 in kur)
        {
            var kur2 = JsonSerializer.Serialize(kur1);
            db.SetAdd("Kurlar", kur2);
        }

        return Task.CompletedTask;
    }

    public List<Kur?>? GetAllKur()
    {
        var cache = _redis.GetDatabase();
        var kurlar = cache.SetMembers("Kurlar");
        if (kurlar.Length > 0)
        {
            var obj = Array.ConvertAll(kurlar, val => JsonSerializer.Deserialize<Kur>(val)).ToList();
            return obj;
        }
        return null;
    }
}
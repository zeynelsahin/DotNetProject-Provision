using Business.API.Models;
using Business.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Business.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class KurCacheController : Controller
{
    private readonly RedisRepository _redisRepository ;

    private readonly HttpClient _httpClient= new HttpClient();
    // GET
    public KurCacheController(RedisRepository redisRepository)
    {
        _redisRepository = redisRepository;
    }

    [HttpGet("GetAllRedisCache")]
    public ActionResult GetAll()
    {
        var kurlar = _redisRepository.GetAllKur();
        return kurlar.Count>0 ? Ok(kurlar) : Ok("Cache içerisinde kular verisi bulunamadı");
    }
    
    [HttpPost("RedisCacheCreate")]
    public async Task<IActionResult> CreatePlatform()
    {
        var result=  await _httpClient.GetFromJsonAsync<List<Kur>>("http://dataapi/api/Kur/GetAll");
        await _redisRepository.CreateKurList(result);
        return Ok("Veriler cach e eklendi");
    }
}
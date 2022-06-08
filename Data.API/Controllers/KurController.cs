using System.Xml;
using Data.API.DBContext;
using Data.API.Entity;
using Data.API.Extensions;
using Data.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Data.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KurController : Controller
{
    // private HttpClient _httpClient = new HttpClient();

    // [HttpGet]
    // // GET
    // public async Task<IActionResult> Result()
    // {
    //     var options = new JsonSerializerOptions
    //     {
    //         Encoder = JavaScriptEncoder.Create(UnicodeRanges.All, UnicodeRanges.All),
    //         WriteIndented = true
    //     };
    //
    //     var data = _httpClient.GetFromJsonAsync<ResponseApi>("https://evds2.tcmb.gov.tr/service/evds/series=TP.DK.USD.S.YTL&startDate=03-04-2022&endDate=03-06-2022&type=json&key=Hje4pUpg8o", options);
    //     var dataa = 1;
    //     return Ok(data);
    // }
    //
    // [HttpGet("deneme")]
    // public IActionResult Result2()
    // {
    //     XmlDocument doc = new XmlDocument();
    //     doc.LoadXml("https://evds2.tcmb.gov.tr/service/evds/series=TP.DK.USD.S.YTL&startDate=03-04-2022&endDate=03-06-2022&type=xml&key=Hje4pUpg8o");
    //
    //     var json = JsonConvert.SerializeXmlNode(doc);
    //     return Ok(json);
    // }  
    // [HttpGet("converter")]
    // public IActionResult Result4()
    // {
    //     var doc =  XDocument.Parse("https://evds2.tcmb.gov.tr/service/evds/series=TP.DK.USD.S.YTL&startDate=03-04-2022&endDate=03-06-2022&type=xml&key=Hje4pUpg8o");
    //     
    //
    //     var json = JsonConvert.SerializeXNode(doc);
    //     return Ok(json);
    // }

    [HttpGet("GetXml")]
    public ResponseApi Result3()
    {
        XmlDocument doc = new XmlDocument();

        doc.Load("https://evds2.tcmb.gov.tr/service/evds/series=TP.DK.USD.S.YTL&startDate=03-04-2022&endDate=03-06-2022&type=xml&key=Hje4pUpg8o");
        ResponseApi responseApi = new ResponseApi();
        List<Item> list = new List<Item>();

        foreach (XmlNode node in doc.SelectNodes("/document"))
        {
            responseApi.TotalCount = Int32.Parse(node["totalCount"].InnerText);
            foreach (XmlNode node1 in doc.SelectNodes("document/items"))
            {
                Item item = new Item()
                {
                    Tarih = node1["Tarih"].InnerText,
                    TP_DK_USD_S_YTL = node1["TP_DK_USD_S_YTL"].InnerText,
                    UNIXTIME = new Unixtime() { NumberLong = node1["UNIXTIME"].InnerText }
                };
                list.Add(item);
            }
        }

        responseApi.Items = list;
        return responseApi;
    }

    [HttpGet("DatabaseSave")]
    public async Task<IActionResult> Save()
    {
        var result = Result3();
        var entities = result.ConvertToAdd();
        await using Context context = new Context();
        await context.Kurlar.AddRangeAsync(entities);
        await context.SaveChangesAsync();
        return Ok("Verileri veritabanına kaydedildi");
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        using Context context = new Context();
        var entities = context.Kurlar.ToList();
        return entities.Count > 0 ? Ok(entities) : Ok("Veritabanında kayıtlı kur bulunamadı");
    }
    
    [HttpGet("GetAll1")]
    public IActionResult GetAll1()
    {
        List<Kur1> entities = new List<Kur1>()
        {
            new Kur1() {Date = DateTime.Parse("2022-06-08"),Close = 58},
            new Kur1() {Date = DateTime.Parse("2022-06-07"),Close = 100},
            new Kur1() {Date = DateTime.Parse("2022-06-05"),Close = 200},
            new Kur1() {Date = DateTime.Parse("2022-06-04"),Close = 300},
        };
        return entities.Count > 0 ? Ok(entities) : Ok("Veritabanında kayıtlı kur bulunamadı");
    }

}

public class Kur1
{
    public DateTime Date { get; set; }
    public int Close { get; set; }
}
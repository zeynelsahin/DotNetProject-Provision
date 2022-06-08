using Data.API.Entity;
using Data.API.Models;

namespace Data.API.Extensions;

public static class DbConverter
{
   public static List<Kur> ConvertToAdd(this ResponseApi responseApi)
   {
       return responseApi.Items.Select(item => new Kur() { Tarih = item.Tarih, NumberLong = item.UNIXTIME.NumberLong, TP_DK_USD_S_YTL = item.TP_DK_USD_S_YTL }).ToList();
   }
}
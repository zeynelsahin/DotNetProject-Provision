using Data.API.Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.API.DBContext;

public class Context: DbContext
{
    public DbSet<Kur> Kurlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connection = "Server=database;Port:Database=Kurlar;User=root;Password=123456";
        optionsBuilder.UseMySql(connection,ServerVersion.AutoDetect(connection));
    }
}
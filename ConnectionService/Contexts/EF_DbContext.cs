using ConnectionService.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectionService.Contexts;

public class EfDbContext : DbContext
{
    
    public DbSet<TableInfo> TableInfos { get; set; }
    public DbSet<ColumnInfo> ColumnInfos { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        string connectionString = "Host=http://localhost;Port=32768;Database=postgres;Username=postgres;Password=postgrespw";
        optionsBuilder.UseNpgsql(connectionString);
        Console.Out.WriteLine("DB connect done");
    }
    
    
    
}
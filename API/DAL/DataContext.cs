using System.Reflection;
using DAL.Accounts.Admins;
using DAL.Accounts.Users;
using DAL.Foods;
using DAL.StaticFiles;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class DataContext(
    DbContextOptions options,
    Config config
) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        options.UseNpgsql(
            config.Database.ConnectionString,
            builder => { builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DataContext))!);
    }
    
    internal DbSet<AdminEntity> Admins => Set<AdminEntity>();
    internal DbSet<UserEntity> Users => Set<UserEntity>();
    internal DbSet<FoodEntity> Foods => Set<FoodEntity>();
    internal DbSet<StaticFileEntity> StaticFiles => Set<StaticFileEntity>();
}
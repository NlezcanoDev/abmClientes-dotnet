using Amb.Clientes.Application.Admin;
using Amb.Clientes.Application.Configuration;
using Amb.Clientes.Persistence.Database;
using Amb.Clientes.Persistence.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Amb.Clientes;

public static class DependencyInjectionService
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        #region Application
        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new MapperProfile());
        });
        services.AddSingleton(mapper.CreateMapper());

        #endregion

        #region Persistence
        services.AddDbContext<DatabaseService>(opt =>
            opt.UseSqlServer(configuration["SQLConnectionString"]));

        services.AddScoped<DatabaseService>();

        services.AddScoped<IClienteRepository, ClienteRepository>();
        #endregion
        
        return services;
    }
}
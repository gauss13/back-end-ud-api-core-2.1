using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreAngular.Extensions
{
    public static class ServiceExtensions
    {
        // Injeccion de Dependencias
        // Scope => Se agrega una instaciona por cada peticion http, es decir, dentro de la misma peticion siempre sera la misma instancia

        // Singleton => misma instancia durante la aplicacion, se mantiene en memoria, siempre y cuando no se reinicie el serivor que seria cuando esa instancia cambiaría
        // distintas peticios http van a compartir la misma instancia de la clase

        // Transient => Transitorio - siempre se realizará nueva instanciacion de la clase, es decir, en cada llamada que se haga se hara una nueva instanciacion aun siendo la misma peticion http

        //EF
        public static void ConfigureEF(this IServiceCollection services, IConfiguration config)
        {
            //services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseSqlServer(config.GetConnectionString("DefaultConecction"));
            //}
            //);


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConecction"),
                    b => b.MigrationsAssembly("ApiCoreAngular"));
            }
           );

        }

        //Identity
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Usuario, IdentityRole>(options =>
            {

                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;

                // Email unico
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
                


            })
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();
        }

        // Repositorio
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            // Cuando en alguna clase te pidan IRepositoryWrapper, 
            // entregale una implementacion u objeto de la siguiente clase RepositoryWrapper
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        // Implementacion en el IIS
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => {
                // por el momento no agregamos ninguna propiedad
                // porque usaremos las predeterminadas

            });
        }

    }
}

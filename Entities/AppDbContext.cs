using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;


namespace Entities
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // Este metodo nos servira para indicar una serie de configuraciones para nuestro context
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //indicarle que queremos utilizar SQL SERVER - aunque este paso lo hacemos desde el startup
        //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-IDV598Q\\SQLEXPRESS;Initial Catalog=AngularAPI;User ID=sa;Password=admin01;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
        //        options =>
        //    {

        //    })

        //    // idicarle que los query se muestren en la consola
        //    .EnableSensitiveDataLogging(true)
        //    .UseLoggerFactory(new LoggerFactory().AddConsole( (category, level)=> level == LogLevel.Information && category == DbLoggerCategory.Database.Command.Name, true ));


        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Usuario>(entity =>
        //    {
        //        //entity.ToTable(name: "Users");
        //        entity.Property(e => e.UserName).HasColumnName("Nombre");
        //        entity.Property(e => e.PasswordHash).HasColumnName("Password");

        //    });


        //}


        public DbSet<Hospital> Hospitales { get; set; }
        public DbSet<Medico> Medicos { get; set; }


    }
}

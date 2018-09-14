using System.Text;
using ApiCoreAngular.Extensions;
using ApiCoreAngular.Filtros;
using ApiCoreAngular.Policies;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ApiCoreAngular
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Injeccion de Dependencias
        public void ConfigureServices(IServiceCollection services)
        {
            // Habilitar CORS
            services.AddCors(options => {

                options.AddPolicy("EnableCORS", builder => {

                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });

            });


            // JWT - middleware
            // especificar el esquema de autenticacion
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //El token será valido si :
                        ValidateIssuer = true, // el emisor es el servidor real que creo el token
                        ValidateAudience = true, // el receptor es un destinatario valido
                        ValidateLifetime = true, // el token no ha expirado
                        ValidateIssuerSigningKey = true, // la clave es valida y el servidor confie en ella

                        ValidIssuer = "http://localhost:59183",
                        ValidAudience = "http://localhost:59183",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                    };
                }
                );

            // GOOGLE Lognin

            //services.AddAuthentication().AddGoogle(googleOptions => {
            //    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
            //    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];

            //    googleOptions.Events.
               


                //googleOptions.Events  = new OpenIdConnectEvents()
                //{
                //    OnRedirectToIdentityProvider = (context) =>
                //    {
                //        if (context.Request.Path != "/account/external")
                //        {
                //            context.Response.Redirect("/account/login");
                //            context.HandleResponse();
                //        }

                //        return Task.FromResult(0);
                //    }
                //}

           // });

            // EF
            services.ConfigureEF(Configuration);

            //Identity
            services.ConfigureIdentity();

            //Filtros personalizado para validar el modelo
            services.AddScoped<ModelValidationAttribute>();

            // para responder cuando el formato de peticion no es aceptada por nuestro servicio
            //config.ReturnHttpNotAcceptable = true;

            // Repository
            services.ConfigureRepositoryWrapper();

            #region POLICIES - REGLA DE VALIDACION
            // Policy - Regla 

            // Forma de aplicar validacion por Claims 
            //services.AddAuthorization( options => {
            //    options.AddPolicy("PolicyCategoriaUsuario", pol => pol.RequireClaim("CategoriaUsuario", new string[] { "4","5"}));
            //});

            // OTRA forma de validacion personalizada, creando una clase handlder para crear la regla de validacion
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("PolicyCategoriaUsuario", pol => pol.Requirements.Add(new CategoriaUsuarioRequirement()));
            //});

            // lo agregamos al services como injeccion de dependcias
            // es es para que cuando se implemente IAuthorizationHandler, puedan utilizar CategoriaUsuarioHandler
            //services.AddSingleton<IAuthorizationHandler, CategoriaUsuarioHandler>(); 
            #endregion


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            #region MANEJO DE ERROR 500 - nivel global de la aplicaacion

            //app.ConfigureExceptionHandler(); // desde una clase de extension
            //app.ConfigureCustomException(); // manejo de errores personalizados - desde una extenion

            //app.UseExceptionHandler(config =>
            //{
            //    config.Run(async contexto =>
            //    {
            //        contexto.Response.StatusCode = 500;
            //        contexto.Response.ContentType = "application/json";

            //        var error = contexto.Features.Get<IExceptionHandlerFeature>();
            //        if (error != null)
            //        {
            //            var ex = error.Error;

            //            await contexto.Response.WriteAsync(new ErrorModel()
            //            {
            //                StatusCode = 500,
            //                ErrorMessage = ex.Message
            //            }.ToString(); //ToString() is overridden to Serialize object
            //        }
            //    });
            //});
            #endregion

            #region MANEJO DE LOS ERRORES 400
            //app.Use(async (contexto, next) =>
            //{
            //    await next();

            //    if (contexto.Response.StatusCode == 404
            //        && !Path.HasExtension(contexto.Request.Path.Value))
            //    {
            //        contexto.Request.Path = "/index.html";// redirege al index del proyecto en angular
            //        await next();
            //    }
            //});
            #endregion

            // renviará encabezados proxy a la solicitud actual - linux
            //app.UseForwardedHeaders(new ForwardedHeadersOptions {
            //    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
            //});

            app.UseCors("EnableCORS"); // activamos cors para toda la aplicacion
            app.UseAuthentication(); // jwt - activar esta linea para usar nuestro servicio de auntenticacion

            // permite usar archivo estaticos para la solicitud.
            //sino establecemos una ruta, usará la carpeta wwwroot como predeterminados
            //app.UseStaticFiles();

            app.UseMvc();

            #region FORMATEADOR XML
            // Activar el formateador en XML
            //services.AddMvc(config =>
            //{
            //    // Add XML Content Negotiation
            //    config.RespectBrowserAcceptHeader = true;// para que acepte los encabezados acept
            //    config.ReturnHttpNotAcceptable = true; // indica al servidor que no acepte peticion en formatos no habilitados, y retorna 406

            //    config.InputFormatters.Add(new XmlSerializerInputFormatter());
            //    config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            //}); 
            #endregion

        }
    }
}

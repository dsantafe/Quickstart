using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using Quickstart.Core.API.Helpers;
using Quickstart.Core.BL.Models;
using Quickstart.Core.BL.Repositories;
using Quickstart.Core.BL.Repositories.Implements;
using System;
using System.Linq;
using System.Text;

namespace Quickstart.Core.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<QuickstartContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddOpenApiDocument(document =>
            {
                document.Title = "Quickstart API";
                document.Description = "El proyecto de muestra es una aplicación construida en .NET Core";

                // CONFIGURAMOS LA SEGURIDAD JWT PARA SWAGGER,
                // PERMITE AÑADIR EL TOKEN JWT A LA CABECERA.
                //document.AddSecurity("JWT", Enumerable.Empty<string>(),
                //    new OpenApiSecurityScheme
                //    {
                //        Type = OpenApiSecuritySchemeType.ApiKey,
                //        Name = "Authorization",
                //        In = OpenApiSecurityApiKeyLocation.Header,
                //        Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}."
                //    });

                //document.OperationProcessors.Add(
                //    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });

            services.AddScoped<IBlogRepository, BlogRepository>();

            // CONFIGURACIÓN DEL SERVICIO DE AUTENTICACIÓN JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JWT:ISSUER_TOKEN"],
                        ValidAudience = Configuration["JWT:AUDIENCE_TOKEN"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["JWT:SECRET_KEY"])
                        )
                    };
                });

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseCors();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            // custom jwt auth middleware
            //app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

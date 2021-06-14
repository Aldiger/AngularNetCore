using Architecture.Web;
using DotNetCore.AspNetCore;
using DotNetCore.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

Host.CreateDefaultBuilder().UseSerilog().Run<Startup>();

namespace Architecture.Web
{
    public sealed class Startup
    {
        public void Configure(IApplicationBuilder application)
        {
            application.UseException();
            application.UseHttps();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            application.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            application.UseRouting();
            application.UseResponseCompression();
            application.UseAuthentication();
            application.UseAuthorization();
            application.UseEndpointsMapControllers();
            application.UseSpa();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSecurity();
            services.AddResponseCompression();
            services.AddControllersWithJsonOptionsAndAuthorizationPolicy();
            // Enable Swagger   
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 3.1 Web API"
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                    }
                });
            });
            services.AddSpa();
            services.AddContext();
            services.AddServices();
        }
    }
}

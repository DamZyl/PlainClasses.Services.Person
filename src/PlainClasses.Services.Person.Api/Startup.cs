using MassTransit;
using System;
using Autofac.Extensions.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using MicroserviceLibrary.Api.Configurations.Extensions;
using MicroserviceLibrary.Api.Utils;
using MicroserviceLibrary.Infrastructure.Databases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlainClasses.Services.Person.Api.Utils;
using PlainClasses.Services.Person.Infrastructure.Sql;

namespace PlainClasses.Services.Person.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSqlConfiguration(Configuration, Consts.DbConfigurationSection);
            services.AddDbContext<PersonContext>();
            services.AddScoped(typeof(IApplicationDbContext), typeof(PersonContext));
            
            services.AddControllers();
            services.AddSwagger();
           
            services.AddErrorHandler();
            
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.UseHealthCheck(provider);
                    config.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                }));
            });
            services.AddMassTransitHostedService();
            
            return new AutofacServiceProvider(AutofacServiceExtension.CreateContainer(services, Configuration, 
                AssembliesConst.MigrationAssembly, AssembliesConst.ApplicationAssembly)); 
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseProblemDetails();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint(Consts.ApiSwaggerUrl, Consts.ApiName);
            });
        }
    }
}

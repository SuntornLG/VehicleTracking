using System;
using System.IO;
using AutoMapper;
using EasyCaching.Core.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using VehicleTracking.Extensions;
using VehicleTracking.Filter;

namespace VehicleTracking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Logger config
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add jwt web token
            services.AuthenticationJwt(Configuration);

            // Connection string
            services.ConfigureConnectionStringService(Configuration);

            // Inject service
            services.ConfigureServiceInjection();

            // API documents
            services.AddSwaggerGenOpenDucument(Configuration);

            //Add AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //Logger service
            services.ConfigureLoggerService();

            services.AddMvc(config => { config.Filters.Add(new ModelFilterAttribute()); });

            services.AddEasyCaching(options => {
                options.UseRedis(redisConfig =>
                {
                    //Setup Endpoint
                    redisConfig.DBConfig.Endpoints.Add(new ServerEndPoint("localhost", 6379));

                    //Setup password if applicable
                    //if (!string.IsNullOrEmpty(serverPassword))
                    //{
                    //    redisConfig.DBConfig.Password = serverPassword;
                    //}
                    //Allow admin operations
                    redisConfig.DBConfig.AllowAdmin = true;
                },"redis1");
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehicle Tracking API V1");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

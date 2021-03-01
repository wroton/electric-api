using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

using Service.Server.Services.Implementations;
using Service.Server.Services.Interfaces;

namespace Service.Server.Configuration
{
    /// <summary>
    /// Configures the application.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">Configuration to use.</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Binds services used by the application.
        /// </summary>
        /// <param name="services">Service collection to populate.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Bind settings.
            services.Configure<ServiceSettings>(_configuration.GetSection("Settings"));

            // Tranient services.
            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();

            // Setup API requirements.
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server", Version = "v1" });
            });
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="applicationBuilder">Application builder used to prepare the application.</param>
        /// <param name="environment">Information about the current environment.</param>
        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment environment)
        {
            // Setup development specific settings.
            if (environment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseSwagger();
                applicationBuilder.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Server v1"));
            }

            // Setup settings for all environments.
            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseRouting();
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

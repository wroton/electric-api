using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;

using Service.Server.Configuration;
using Service.Server.Services.Implementations;
using Service.Server.Services.Interfaces;

namespace Service.Server.Infrastructure
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
            services.Configure<JwtSettings>(_configuration.GetSection("Jwt"));

            // Tranient services.
            services.AddTransient<IBusinessService, BusinessService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
            services.AddTransient<IHashService, HashService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<ITechnicianPositionService, TechnicianPositionService>();
            services.AddTransient<ITechnicianService, TechnicianService>();
            services.AddTransient<IUserService, UserService>();

            // Request scoped services.
            services.AddScoped<IRequestContext, RequestContext>();

            // Allow the http context to be accessed.
            services.AddHttpContextAccessor();

            // Setup MVC options.
            var mvcBuilder = services.AddControllers(mvcOptions =>
            {
                mvcOptions.Filters.Add(typeof(ValidateModelAttribute));
            });

            // Setup JSON options.
            mvcBuilder.AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            // Setup Swagger.
            services.AddSwaggerGen(c =>
            {
                // Setup the document for version 1.
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server", Version = "v1" });

                // Create the security schema for an authorization token.
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "Authorization Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };

                // Define the security definition and set it as a requirement.
                c.AddSecurityDefinition("Authorization", securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, Array.Empty<string>() }
                });
            });

            // Add JWT authentication.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearerConfiguration(_configuration);
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="applicationBuilder">Application builder used to prepare the application.</param>
        /// <param name="environment">Information about the current environment.</param>
        /// <param name="applicationLifetime">Callbacks to use for lifetime events.</param>
        /// <param name="loggerFactory">Logger factory used to build loggers.</param>
        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment environment,
            IHostApplicationLifetime applicationLifetime, ILoggerFactory loggerFactory)
        {
            // Setup development specific settings.
            if (environment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseSwagger();
                applicationBuilder.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Server v1"));
            }

            // Enable CORS.
            applicationBuilder.UseCors(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });

            // Setup settings for all environments.
            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseRouting();
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Create a logger for the application lifetime.
            var logger = loggerFactory.CreateLogger<IHostApplicationLifetime>();

            // Application lifetime events.
            applicationLifetime.ApplicationStarted.Register(() => logger.LogInformation("Application started."));
            applicationLifetime.ApplicationStopping.Register(() => logger.LogInformation("Application stopping."));
            applicationLifetime.ApplicationStopped.Register(() => logger.LogInformation("Application stopped."));
        }
    }
}

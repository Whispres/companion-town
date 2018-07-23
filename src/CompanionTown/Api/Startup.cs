using Api.Options;
using Api.Repositories;
using Api.Repositories.Implementation;
using Api.Services;
using Api.Services.Implementation;
using Hangfire;
using Hangfire.LiteDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace Api
{
    public class Startup
    {
        private readonly string SwaggerApiTitle = "Companion Town API";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

            services.AddOptions();

            services.Configure<DatabaseOptions>(Configuration.GetSection("Database"));

            services.Configure<AnimalJobOptions>(Configuration.GetSection("RecurringJob"));

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IAnimalRepository, AnimalRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAnimalService, AnimalService>();
            services.AddTransient<IAnimalManagementService, AnimalManagementService>();

            services.AddHangfire(t => t.UseLiteDbStorage(Configuration.GetSection("Database")["HangfireConnectionString"]));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = SwaggerApiTitle, Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", SwaggerApiTitle);
            });

            app.UseHangfireServer();

            app.UseHangfireDashboard();

            app.UseMvc();
        }
    }
}
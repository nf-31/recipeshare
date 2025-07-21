using DbUp;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using RecipeShare.Library.BusinessLogic;
using RecipeShare.Library.BusinessLogic.Implementations;
using RecipeShare.Library.EntityFramework;
using RecipeShare.Library.Repositories;
using RecipeShare.Library.Repositories.Implementations;

namespace RecipeShare
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            UpdateDatabase(app, logger);
            
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext, repositories, and business logic
            ConfigureRepositories(services);
            ConfigureBusinessLogic(services);
            services.AddDbContext<RecipeShareDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("Database"),
                    b => b.MigrationsAssembly("RecipeShare.Library")
                ));

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            services.AddSwaggerGen();
        }

        private void ConfigureBusinessLogic(IServiceCollection services)
        {
            services.AddScoped<IRecipeShareBusinessLogic, RecipeShareBusinessLogic>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IRecipeShareRepository, RecipeShareRepository>();
        }
        

        private void UpdateDatabase(IApplicationBuilder app, ILogger<Startup> logger)
        {
            using (var serviceScope = app.ApplicationServices
                       .GetRequiredService<IServiceScopeFactory>()
                       .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<RecipeShareDbContext>())
                {
                    context.Database.Migrate();
                }
            }
            
            logger.LogInformation($"Successfully upgraded database");
        }
    }
}
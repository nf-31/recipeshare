using DbUp;
using System.Reflection;
using RecipeShare.Library.BusinessLogic;
using RecipeShare.Library.BusinessLogic.Implementations;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            ConfigureDatabase(services);

            services.AddControllers();
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

        private void ConfigureDatabase(IServiceCollection services)
        {
            // Run DbUp migrations
            var connectionString = Configuration.GetConnectionString("Database");
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();
            if (!result.Successful)
            {
                throw new Exception("Database migration failed", result.Error);
            }
        }
    }
}
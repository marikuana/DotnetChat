using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using DotnetChat.Data;

namespace DotnetChat
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ChatContext>(options => options.UseSqlServer(connectionString));
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", () => "Hello Chat!");
            });
        }
    }
}
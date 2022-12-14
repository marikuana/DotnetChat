using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using DotnetChat.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using DotnetChat.Controllers;
using System.Reflection;

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
            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            services.AddTransient<IChatManager, ChatManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMessageManager, MessageManager>();

            var onlineService = new OnlineUserService();
            services.AddSingleton<IUserConnertions>(onlineService);
            services.AddSingleton<IOnlineUserService>(onlineService);

            services.AddTransient<IUserSenderService, UserSenderService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Chat}/{action=Index}");
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
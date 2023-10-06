using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.FileManager;
using My_Final_Project.FileManagers;
using My_Final_Project.Implementations.Repositories;
using My_Final_Project.Implementations.Services;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IRoleService, RoleService>();

        builder.Services.AddScoped<ISuperAdminRepository, SuperAdminRepository>();
        builder.Services.AddScoped<ISuperAdminService, SuperAdminService>();

        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IClientService, ClientService>();

        builder.Services.AddScoped<ITherapistRepository, TherapistRepository>();
        builder.Services.AddScoped<ITherapistService, TherapistService>();

        builder.Services.AddScoped<IIssuesRepository, IssuesRepository>();
        builder.Services.AddScoped<IIssuesService, IssuesService>();

        builder.Services.AddScoped<ITherapistIssuesRepository, TherapistIssuesRepository>();
        builder.Services.AddScoped<ITherapistIssuesService, TherapistIssuesService>();

        builder.Services.AddScoped<IBookingRepository, BookingRepository>();
        builder.Services.AddScoped<IBookingService, BookingService>();

        builder.Services.AddScoped<IChatRepository, ChatRepository>();
        builder.Services.AddScoped<IChatService, ChatService>();

        builder.Services.AddScoped<IFileManager, FileManager>();
        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 9; // Password length requirements
                                                 // Configure other password requirements here
        })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
        builder.Services.AddScoped<INotificationMessage, NotificationMessage>();
        builder.Services.AddOptions<WhatsappMessageSettings>().BindConfiguration(nameof(WhatsappMessageSettings));
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(config =>
        {
            config.LoginPath = "/User/Login";
            config.LogoutPath = "/Home/Index";
            config.ExpireTimeSpan = TimeSpan.FromHours(1);
            config.Cookie.Name = "MyFinalProject";
        });

        builder.Services.AddHangfire(config =>
        config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseDefaultTypeSerializer()
        .UseMemoryStorage());
        builder.Services.AddHangfireServer();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        _ = app.Seed();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseHangfireDashboard();

        app.UseAuthorization();
        app.UseAuthentication();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");


        app.Run();
    }
}
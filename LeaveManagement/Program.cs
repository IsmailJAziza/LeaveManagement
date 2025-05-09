using LeaveManagement.Data.DataModel;
using LeaveManagement.Services;
using LeaveManagement.Services.Interface;
using LeaveManagement.Services.InterFace;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LeaveManagement;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        builder.Services.AddScoped<IPeriodRepository, PeriodRepository>();
        builder.Services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminSupervisor", policy => {
                policy.RequireRole(Roles.Administrator, Roles.Supervisor);
            });
        });

        builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
           .AddRoles<IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();
        app.MapRazorPages()
           .WithStaticAssets();

        app.Run();
    }
}

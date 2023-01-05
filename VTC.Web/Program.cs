using VTC.Data;
using Microsoft.EntityFrameworkCore;
using VTC.Application.Services.Interfaces;
using VTC.Application.Services;
using VTC.Data.Repositories.Interfaces;
using VTC.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using VTC.Data.Entities;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using VTC.Application.ViewModels;

namespace VTC.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
           

        var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //var connectionstring = builder.Configuration.GetConnectionString("");
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<VTCDataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("VTCConnectionstring")));
            builder.Services.AddScoped<IPackageService, PackageService>();
            builder.Services.AddScoped<ILevelService, LevelService>();
            builder.Services.AddScoped<ISubjectService, SubjectService>();
            builder.Services.AddScoped<IPackageRepository, PackageRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ILevelRepository, LevelRepository>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<IFormService,FormService>();
            builder.Services.AddScoped<IFormRepository, FormRepository>();
            builder.Services.AddScoped<ILessonRepository, LessonRepository>();
            builder.Services.AddScoped<ILessonService, LessonService>();

            builder.Services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<VTCDataContext>();
           
                           
            //    builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            //.AddEntityFrameworkStores<IdentityDbContext>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            
         

            app.MapControllerRoute(
                name: "default",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
          
    }
}
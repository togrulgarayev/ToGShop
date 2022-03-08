using AutoMapper;
using Business.Implementations;
using Business.Interfaces;
using Business.Validators.Product;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Data.DAL;
using Data.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

namespace ToGShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddFluentValidation(p => p.RegisterValidatorsFromAssemblyContaining<ProductCreateViewModelValidator>());
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:Default"]);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(Options =>
            {
                Options.Password.RequiredLength = 8;
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireDigit = true;


                Options.User.AllowedUserNameCharacters = "abcçdeəfgğhiıjklmnopqrsştuvwxyzABCÇDEƏFGĞHİIJKLMNOPQRSŞTUVWXYZ0123456789-._@+/ ";
            });

            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "747003403352509";
                options.AppSecret = "09ae3b4a78d9087a00ff5ea15247cd99";
            });

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "972970270872-e7mqi91turg3d1euome2tf8uji98setv.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });

            services.AddScoped<IProductService, ProductService>();  
            services.AddScoped<ICategoryService, CategoryService>();  
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductImageService, ProductImageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

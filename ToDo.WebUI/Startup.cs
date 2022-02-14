using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDo.Business.Concrete;
using ToDo.Business.Interfaces;
using ToDo.Business.ValidationRules.FluentValidation;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using ToDo.DataAccess.Interfaces;
using ToDo.DTO.DTOs.AciliyetDtos;
using ToDo.DTO.DTOs.AppUserDtos;
using ToDo.DTO.DTOs.GorevDtos;
using ToDo.DTO.DTOs.RaporDtos;
using ToDo.Entities.Concrete;

namespace ToDo.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGorevService, GorevManager>();
            services.AddScoped<IGorevDal, EfGorevRepository>();

            services.AddScoped<IAciliyetService, AciliyetManager>();
            services.AddScoped<IAciliyetDal, EfAciliyetRepository>();

            services.AddScoped<IRaporService, RaporManager>();
            services.AddScoped<IRaporDal, EfRaporRepository>();

            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IAppUserDal, EfAppUserRepository>();

            services.AddScoped<IBildirimService, BildirimManager>();
            services.AddScoped<IBildirimDal, EfBildirimRepository>();

            services.AddScoped<IDosyaService, DosyaManager>();

            services.AddDbContext<TodoContext>();
            services.AddIdentity<AppUser, AppRole>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredLength = 4;
                option.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<TodoContext>();

            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.Name = "ApplicationCookie";
                option.Cookie.SameSite = SameSiteMode.Strict;
                option.Cookie.HttpOnly = true;
                option.ExpireTimeSpan = System.TimeSpan.FromDays(20);
                option.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

                option.LoginPath = "/Home/GirisYap";
                option.LogoutPath = "/Home/KayitOl";
                option.AccessDeniedPath = "/Home/AccessDenied";
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IValidator<AciliyetAddDto>, AciliyetAddValidator>();
            services.AddTransient<IValidator<AciliyetUpdateDto>, AciliyetUpdateValidator>();
            services.AddTransient<IValidator<AppUserAddDto>, AppUserAddValidator>();
            services.AddTransient<IValidator<AppUserSignInDto>, AppUserSignInValidator>();
            services.AddTransient<IValidator<GorevAddDto>, GorevAddValidator>();
            services.AddTransient<IValidator<GorevUpdateDto>, GorevUpdateValidator>();
            services.AddTransient<IValidator<RaporAddDto>, RaporAddValidator>();
            services.AddTransient<IValidator<RaporUpdateDto>, RaporUpdateValidator>();

            services.AddControllersWithViews().AddFluentValidation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            IdentityInitializer.SeedData(userManager, roleManager).Wait();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}

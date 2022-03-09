using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Business.ValidationRules.FluentValidation;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using ToDo.DTO.DTOs.AciliyetDtos;
using ToDo.DTO.DTOs.AppUserDtos;
using ToDo.DTO.DTOs.GorevDtos;
using ToDo.DTO.DTOs.RaporDtos;
using ToDo.Entities.Concrete;

namespace ToDo.WebUI.CustomCollectionExtensions
{
    public static class CollectionExtension
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
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
        }

        public static void AddCustomValidator(this IServiceCollection services)
        {
            services.AddTransient<IValidator<AciliyetAddDto>, AciliyetAddValidator>();
            services.AddTransient<IValidator<AciliyetUpdateDto>, AciliyetUpdateValidator>();
            services.AddTransient<IValidator<AppUserAddDto>, AppUserAddValidator>();
            services.AddTransient<IValidator<AppUserSignInDto>, AppUserSignInValidator>();
            services.AddTransient<IValidator<GorevAddDto>, GorevAddValidator>();
            services.AddTransient<IValidator<GorevUpdateDto>, GorevUpdateValidator>();
            services.AddTransient<IValidator<RaporAddDto>, RaporAddValidator>();
            services.AddTransient<IValidator<RaporUpdateDto>, RaporUpdateValidator>();
        }
    }
}

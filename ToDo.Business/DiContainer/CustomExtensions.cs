using Microsoft.Extensions.DependencyInjection;
using ToDo.Business.Concrete;
using ToDo.Business.Interfaces;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using ToDo.DataAccess.Interfaces;

namespace ToDo.Business.DiContainer
{
    public static class CustomExtensions
    {
        public static void AddContainerWithDependencies(this IServiceCollection services)
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
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfBildirimRepository : EfGenericRepository<Bildirim>, IBildirimDal
    {
        public List<Bildirim> GetirOkunmayanlar(int appUserId)
        {
            using TodoContext context = new();
            return context.Bildirimler
                .Where(x => x.AppUserId == appUserId && !x.Durum)
                .ToList();
        }

        public int GetirOkunmayanSayisiileAppUserId(int id)
        {
            using TodoContext context = new();
            return context.Bildirimler.Count(I => I.AppUserId == id && !I.Durum);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfRaporRepository : EfGenericRepository<Rapor>, IRaporDal
    {
        public Rapor GetirGorevIleId(int id)
        {
            using TodoContext context = new();
            return context.Raporlar
                .Include(x => x.Gorev)
                .ThenInclude(x => x.Aciliyet)
                .FirstOrDefault(x => x.Id == id);
        }

        public int GetirRaporSayisiileAppUserId(int id)
        {
            using TodoContext context = new();
            var result = context.Gorevler
                .Include(I => I.Raporlar)
                .Where(I => I.AppUserId == id);

            return result
                .SelectMany(I => I.Raporlar)
                .Count();
        }
    }
}

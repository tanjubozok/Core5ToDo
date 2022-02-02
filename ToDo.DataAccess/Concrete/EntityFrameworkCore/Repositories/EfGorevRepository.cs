using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGorevRepository : EfGenericRepository<Gorev>, IGorevDal
    {
        public Gorev GetirAciliyetIleId(int id)
        {
            using TodoContext context = new();
            return context.Gorevler
                  .Include(x => x.Aciliyet)
                  .FirstOrDefault(x => x.Durum == false && x.Id == id);
        }

        public List<Gorev> GetirAciliyetIleTamamlanmayanlari()
        {
            using TodoContext context = new();
            return context.Gorevler
                  .Include(x => x.Aciliyet)
                  .Where(x => x.Durum == false)
                  .OrderByDescending(x => x.OlusturmaTarihi).ToList();
        }

        public Gorev GetirRaporlarIleId(int id)
        {
            using TodoContext context = new();
            return context.Gorevler
                .Include(x => x.Raporlar)
                .Include(x => x.AppUser)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Gorev> GetirAppUserId(int appUserId)
        {
            using TodoContext context = new();
            return context.Gorevler
                .Where(x => x.AppUserId == appUserId)
                .ToList();
        }

        public List<Gorev> GetirTumTablolarla()
        {
            using TodoContext context = new();
            return context.Gorevler
                  .Include(x => x.Aciliyet)
                  .Include(x => x.Raporlar)
                  .Include(x => x.AppUser)
                  .Where(x => x.Durum == false)
                  .OrderByDescending(x => x.OlusturmaTarihi).ToList();
        }
    }
}

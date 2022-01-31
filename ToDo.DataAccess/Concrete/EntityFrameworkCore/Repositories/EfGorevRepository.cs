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
        public List<Gorev> GetirAciliyetIleTamamlanmayanlari()
        {
            using var context = new TodoContext();
            return context.Gorevler
                  .Include(x => x.Aciliyet)
                  .Where(x => x.Durum == false)
                  .OrderByDescending(x => x.OlusturmaTarihi).ToList();
        }

        public List<Gorev> GetirTumTablolarla()
        {
            using var context = new TodoContext();
            return context.Gorevler
                  .Include(x => x.Aciliyet)
                  .Include(x => x.Raporlar)
                  .Include(x => x.AppUser)
                  .Where(x => x.Durum == false)
                  .OrderByDescending(x => x.OlusturmaTarihi).ToList();
        }
    }
}

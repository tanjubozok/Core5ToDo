using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Interfaces
{
    public interface IGorevService : IGenericService<Gorev>
    {
        List<Gorev> GetirAciliyetIleTamamlanmayanlari();
        List<Gorev> GetirTumTablolarla();
        List<Gorev> GetirTumTablolarla(Expression<Func<Gorev, bool>> filter);
        List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa1 = 1);
        Gorev GetirAciliyetIleId(int id);
        List<Gorev> GetirAppUserId(int appUserId);
        Gorev GetirRaporlarIleId(int id);
    }
}

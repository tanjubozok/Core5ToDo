using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Interfaces
{
    public interface IGorevDal : IGenericDal<Gorev>
    {
        List<Gorev> GetirAciliyetIleTamamlanmayanlari();
        List<Gorev> GetirTumTablolarla();
        List<Gorev> GetirTumTablolarla(Expression<Func<Gorev, bool>> filter);
        List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa);
        Gorev GetirAciliyetIleId(int id);
        List<Gorev> GetirAppUserId(int appUserId);
        Gorev GetirRaporlarIleId(int id);
        int GetirGorevSayisiTamamlananileAppUserId(int id);
        int GetirGorevSayisiTamamlanmasiGerekenileAppUserId(int id);
    }
}

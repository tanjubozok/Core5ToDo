using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ToDo.Business.Interfaces;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Concrete
{
    public class GorevManager : IGorevService
    {
        private readonly IGorevDal _gorevDal;

        public GorevManager(IGorevDal gorevDal)
        {
            _gorevDal = gorevDal;
        }

        public Gorev GetirAciliyetIleId(int id)
        {
            return _gorevDal.GetirAciliyetIleId(id);
        }

        public List<Gorev> GetirAciliyetIleTamamlanmayanlari()
        {
            return _gorevDal.GetirAciliyetIleTamamlanmayanlari();
        }

        public List<Gorev> GetirAppUserId(int appUserId)
        {
            return _gorevDal.GetirAppUserId(appUserId);
        }

        public int GetirGorevSayisiAtanmayiBekleyen()
        {
            return _gorevDal.GetirGorevSayisiAtanmayiBekleyen();
        }

        public int GetirGorevSayisiTamamlananileAppUserId(int id)
        {
            return _gorevDal.GetirGorevSayisiTamamlananileAppUserId(id);
        }

        public int GetirGorevSayisiTamamlanmasiGerekenileAppUserId(int id)
        {
            return _gorevDal.GetirGorevSayisiTamamlanmasiGerekenileAppUserId(id);
        }

        public int GetirGorevTamamlanmis()
        {
            return _gorevDal.GetirGorevTamamlanmis();
        }

        public List<Gorev> GetirHepsi()
        {
            return _gorevDal.GetirHepsi();
        }

        public Gorev GetirId(int id)
        {
            return _gorevDal.GetirId(id);
        }

        public Gorev GetirRaporlarIleId(int id)
        {
            return _gorevDal.GetirRaporlarIleId(id);
        }

        public List<Gorev> GetirTumTablolarla()
        {
            return _gorevDal.GetirTumTablolarla();
        }

        public List<Gorev> GetirTumTablolarla(Expression<Func<Gorev, bool>> filter)
        {
            return _gorevDal.GetirTumTablolarla(filter);
        }

        public List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa)
        {
            return _gorevDal.GetirTumTablolarlaTamamlanmayan(out toplamSayfa, userId, aktifSayfa);
        }

        public void Guncelle(Gorev model)
        {
            _gorevDal.Guncelle(model);
        }

        public void Kaydet(Gorev model)
        {
            _gorevDal.Kaydet(model);
        }

        public void Sil(Gorev model)
        {
            _gorevDal.Sil(model);
        }
    }
}

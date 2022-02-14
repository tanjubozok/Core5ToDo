using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Concrete
{
    public class RaporManager : IRaporService
    {
        private readonly IRaporDal _raporDal;

        public RaporManager(IRaporDal raporDal)
        {
            _raporDal = raporDal;
        }

        public Rapor GetirGorevIleId(int id)
        {
            return _raporDal.GetirGorevIleId(id);
        }

        public List<Rapor> GetirHepsi()
        {
            return _raporDal.GetirHepsi();
        }

        public Rapor GetirId(int id)
        {
            return _raporDal.GetirId(id);
        }

        public int GetirRaporSayisi()
        {
            return _raporDal.GetirRaporSayisi();
        }

        public int GetirRaporSayisiileAppUserId(int id)
        {
            return _raporDal.GetirRaporSayisiileAppUserId(id);
        }

        public void Guncelle(Rapor model)
        {
            _raporDal.Guncelle(model);
        }

        public void Kaydet(Rapor model)
        {
            _raporDal.Kaydet(model);
        }

        public void Sil(Rapor model)
        {
            _raporDal.Sil(model);
        }
    }
}

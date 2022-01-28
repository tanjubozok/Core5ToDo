using System.Collections.Generic;
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

        public List<Gorev> GetirAciliyetIleTamamlanmayanlari()
        {
            return _gorevDal.GetirAciliyetIleTamamlanmayanlari();
        }

        public List<Gorev> GetirHepsi()
        {
            return _gorevDal.GetirHepsi();
        }

        public Gorev GetirId(int id)
        {
            return _gorevDal.GetirId(id);
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

using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Concrete
{
    public class AciliyetManager : IAciliyetService
    {
        private readonly IAciliyetDal _aciliyetDal;
        public AciliyetManager(IAciliyetDal aciliyetDal)
        {
            _aciliyetDal = aciliyetDal;
        }

        public List<Aciliyet> GetirHepsi()
        {
            return _aciliyetDal.GetirHepsi();
        }

        public Aciliyet GetirId(int id)
        {
            return _aciliyetDal.GetirId(id);
        }

        public void Guncelle(Aciliyet model)
        {
            _aciliyetDal.Guncelle(model);
        }

        public void Kaydet(Aciliyet model)
        {
            _aciliyetDal.Kaydet(model);
        }

        public void Sil(Aciliyet model)
        {
            _aciliyetDal.Sil(model);
        }
    }
}

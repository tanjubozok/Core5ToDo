using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Concrete
{
    public class CalismaManager : ICalismaService
    {
        private readonly EfCalismaRepository _efCalismaRepository;

        public CalismaManager()
        {
            _efCalismaRepository = new EfCalismaRepository();
        }

        public List<Calisma> GetirHepsi()
        {
            return _efCalismaRepository.GetirHepsi();
        }

        public Calisma GetirId(int id)
        {
            return _efCalismaRepository.GetirId(id);
        }

        public void Guncelle(Calisma model)
        {
            _efCalismaRepository.Guncelle(model);
        }

        public void Kaydet(Calisma model)
        {
            _efCalismaRepository.Kaydet(model);
        }

        public void Sil(Calisma model)
        {
            _efCalismaRepository.Sil(model);
        }
    }
}

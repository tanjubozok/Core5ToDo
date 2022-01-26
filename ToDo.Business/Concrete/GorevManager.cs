using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Concrete
{
    public class GorevManager : IGorevService
    {
        private readonly EfGorevRepository _efCalismaRepository;

        public GorevManager()
        {
            _efCalismaRepository = new EfGorevRepository();
        }

        public List<Gorev> GetirHepsi()
        {
            return _efCalismaRepository.GetirHepsi();
        }

        public Gorev GetirId(int id)
        {
            return _efCalismaRepository.GetirId(id);
        }

        public void Guncelle(Gorev model)
        {
            _efCalismaRepository.Guncelle(model);
        }

        public void Kaydet(Gorev model)
        {
            _efCalismaRepository.Kaydet(model);
        }

        public void Sil(Gorev model)
        {
            _efCalismaRepository.Sil(model);
        }
    }
}

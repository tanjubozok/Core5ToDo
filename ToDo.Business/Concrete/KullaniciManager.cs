using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Concrete
{
    public class KullaniciManager : IKullaniciService
    {
        private readonly EfKullaniciRepository _efKullaniciRepository;

        public KullaniciManager()
        {
            _efKullaniciRepository = new EfKullaniciRepository();
        }

        public List<Kullanici> GetirHepsi()
        {
            return _efKullaniciRepository.GetirHepsi();
        }

        public Kullanici GetirId(int id)
        {
            return _efKullaniciRepository.GetirId(id);
        }

        public void Guncelle(Kullanici model)
        {
            _efKullaniciRepository.Guncelle(model);
        }

        public void Kaydet(Kullanici model)
        {
            _efKullaniciRepository.Kaydet(model);
        }

        public void Sil(Kullanici model)
        {
            _efKullaniciRepository.Sil(model);
        }
    }
}

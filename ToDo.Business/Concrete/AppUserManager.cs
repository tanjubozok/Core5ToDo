using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserDal _userDal;

        public AppUserManager(IAppUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<AppUser> GetirAdminOlmayanlar()
        {
            return _userDal.GetirAdminOlmayanlar();
        }

        public List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa, string arabacakKelime, int aktifSayfa)
        {
            return _userDal.GetirAdminOlmayanlar(out toplamSayfa, arabacakKelime, aktifSayfa);
        }

        public List<DualHelper> GetirEnCokGorevdeCalisanPersoneller()
        {
            return _userDal.GetirEnCokGorevdeCalisanPersoneller();
        }

        public List<DualHelper> GetirEnCokGorevTamamlamisPersoneller()
        {
            return _userDal.GetirEnCokGorevTamamlamisPersoneller();
        }
    }
}

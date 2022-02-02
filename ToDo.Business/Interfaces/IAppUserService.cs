using System.Collections.Generic;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Interfaces
{
    public interface IAppUserService
    {
        List<AppUser> GetirAdminOlmayanlar();
        List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa, string arabacakKelime, int aktifSayfa);
    }
}

using System.Collections.Generic;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Interfaces
{
    public interface IAppUserDal
    {
        List<AppUser> GetirAdminOlmayanlar();
    }
}

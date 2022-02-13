using System.Collections.Generic;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Interfaces
{
    public interface IBildirimService : IGenericService<Bildirim>
    {
        List<Bildirim> GetirOkunmayanlar(int appUserId);
        int GetirOkunmayanSayisiileAppUserId(int id);
    }
}

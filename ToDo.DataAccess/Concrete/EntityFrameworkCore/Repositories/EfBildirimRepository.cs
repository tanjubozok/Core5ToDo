using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfBildirimRepository : EfGenericRepository<Bildirim>, IBildirimDal
    {
    }
}

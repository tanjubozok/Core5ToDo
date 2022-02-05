using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Interfaces
{
    public interface IRaporDal : IGenericDal<Rapor>
    {
        Rapor GetirGorevIleId(int id);
    }
}

using ToDo.Entities.Concrete;

namespace ToDo.Business.Interfaces
{
    public interface IRaporService : IGenericService<Rapor>
    {
        Rapor GetirGorevIleId(int id);
        int GetirRaporSayisiileAppUserId(int id);
    }
}

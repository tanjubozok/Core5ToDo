using System.Collections.Generic;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Interfaces
{
    public interface IGorevService : IGenericService<Gorev>
    {
        List<Gorev> GetirAciliyetIleTamamlanmayanlari();
        List<Gorev> GetirTumTablolarla();
        Gorev GetirAciliyetIleId(int id);
        List<Gorev> GetirAppUserId(int appUserId);
        Gorev GetirRaporlarIleId(int id);
    }
}

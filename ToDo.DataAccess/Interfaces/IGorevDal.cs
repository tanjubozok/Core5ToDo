using System.Collections.Generic;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Interfaces
{
    public interface IGorevDal : IGenericDal<Gorev>
    {
        List<Gorev> GetirAciliyetIleTamamlanmayanlari();
    }
}

using System.Collections.Generic;
using ToDo.Entities.Interfaces;

namespace ToDo.Business.Interfaces
{
    public interface IGenericService<T> where T : class, ITable, new()
    {
        void Kaydet(T model);
        void Sil(T model);
        void Guncelle(T model);
        T GetirId(int id);
        List<T> GetirHepsi();
    }
}

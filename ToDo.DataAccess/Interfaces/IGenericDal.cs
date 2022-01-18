using System.Collections.Generic;
using ToDo.Entities.Interfaces;

namespace ToDo.DataAccess.Interfaces
{
    public interface IGenericDal<T> where T : class, ITable, new()
    {
        void Kaydet(T model);
        void Sil(T model);
        void Guncelle(T model);
        T GetirId(int id);
        List<T> GetirHepsi();
    }
}

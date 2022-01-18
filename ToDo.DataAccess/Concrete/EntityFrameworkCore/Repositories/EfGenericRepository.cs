using System.Collections.Generic;
using System.Linq;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Interfaces;

namespace ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGenericRepository<T> : IGenericDal<T>
        where T : class, ITable, new()
    {
        public List<T> GetirHepsi()
        {
            using var context = new TodoContext();
            return context.Set<T>().ToList();
        }

        public T GetirId(int id)
        {
            using var context = new TodoContext();
            return context.Set<T>().Find(id);
        }

        public void Guncelle(T model)
        {
            using var context = new TodoContext();
            context.Set<T>().Update(model);
            context.SaveChanges();
        }

        public void Kaydet(T model)
        {
            using var context = new TodoContext();
            context.Set<T>().Add(model);
            context.SaveChanges();
        }

        public void Sil(T model)
        {
            using var context = new TodoContext();
            context.Set<T>().Remove(model);
            context.SaveChanges();
        }
    }
}

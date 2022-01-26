using Microsoft.EntityFrameworkCore;
using ToDo.Entities.Interfaces;

namespace ToDo.Entities.Concrete
{
    public class Aciliyet : ITable
    {
        public int Id { get; set; }
        public string Tanim { get; set; }

        public DbSet<Gorev> Gorevler { get; set; }
    }
}

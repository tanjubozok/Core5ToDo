using System.Collections.Generic;
using ToDo.Entities.Interfaces;

namespace ToDo.Entities.Concrete
{
    public class Aciliyet : ITable
    {
        public int Id { get; set; }
        public string Tanim { get; set; }

        public List<Gorev> Gorevler { get; set; }
    }
}

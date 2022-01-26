using ToDo.Entities.Interfaces;

namespace ToDo.Entities.Concrete
{
    public class Rapor : ITable
    {
        public int Id { get; set; }
        public string Tanim { get; set; }
        public string Detay { get; set; }

        public int GorevId { get; set; }
        public Gorev Gorev { get; set; }
    }
}

using ToDo.Entities.Interfaces;

namespace ToDo.Entities.Concrete
{
    public class Bildirim : ITable
    {
        public int Id { get; set; }
        public string Aciklama { get; set; }
        public bool Durum { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}

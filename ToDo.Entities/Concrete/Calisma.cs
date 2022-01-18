using System;
using ToDo.Entities.Interfaces;

namespace ToDo.Entities.Concrete
{
    public class Calisma : ITable
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool Durum { get; set; }
        public DateTime OlusturmaTarihi { get; set; }

        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }
    }
}

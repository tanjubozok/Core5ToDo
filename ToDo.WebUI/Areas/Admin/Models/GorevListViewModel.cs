using System;
using ToDo.Entities.Concrete;

namespace ToDo.WebUI.Areas.Admin.Models
{
    public class GorevListViewModel
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool Durum { get; set; }
        public DateTime OlusturmaTarihi { get; set; }

        public int AciliyetId { get; set; }
        public Aciliyet Aciliyet { get; set; }
    }
}

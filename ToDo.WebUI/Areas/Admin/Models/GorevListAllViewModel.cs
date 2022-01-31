using System;
using System.Collections.Generic;
using ToDo.Entities.Concrete;

namespace ToDo.WebUI.Areas.Admin.Models
{
    public class GorevListAllViewModel
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public DateTime OluşturmaTarihi { get; set; }
        public Aciliyet Aciliyet { get; set; }
        public AppUser AppUser { get; set; }
        public List<Rapor> Raporlar { get; set; }
    }
}

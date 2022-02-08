using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using ToDo.Entities.Interfaces;

namespace ToDo.Entities.Concrete
{
    public class AppUser : IdentityUser<int>, ITable
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Picture { get; set; } = "default.png";

        public List<Gorev> Gorevler { get; set; }
        public List<Bildirim> Bildirimler { get; set; }
    }
}

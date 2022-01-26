using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ToDo.Entities.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string SurName { get; set; }

        public List<Gorev> Gorevler { get; set; }
    }
}

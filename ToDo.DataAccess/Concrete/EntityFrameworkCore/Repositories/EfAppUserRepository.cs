using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAppUserRepository : IAppUserDal
    {
        public List<AppUser> GetirAdminOlmayanlar()
        {
            using var context = new TodoContext();
            return (from u in context.Users
                    join ur in context.UserRoles on u.Id equals ur.UserId
                    join r in context.Roles on ur.RoleId equals r.Id
                    where r.Name == "Member"
                    select new AppUser
                    {
                        Id = u.Id,
                        Name = u.Name,
                        SurName = u.SurName,
                        Email = u.Email,
                        UserName = u.UserName
                    }).ToList();
        }

        public List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa, string arabacakKelime, int aktifSayfa)
        {
            using var context = new TodoContext();
            var result = (from u in context.Users
                          join ur in context.UserRoles on u.Id equals ur.UserId
                          join r in context.Roles on ur.RoleId equals r.Id
                          where r.Name == "Member"
                          select new AppUser
                          {
                              Id = u.Id,
                              Name = u.Name,
                              SurName = u.SurName,
                              Email = u.Email,
                              UserName = u.UserName
                          });

            toplamSayfa = (int)Math.Ceiling((double)result.Count() / 3);

            if (!string.IsNullOrWhiteSpace(arabacakKelime))
            {
                result = result
                    .Where(x => x.Name.ToLower()
                    .Contains(arabacakKelime.ToLower())
                    || x.SurName.ToLower()
                    .Contains(arabacakKelime.ToLower()));

                toplamSayfa = (int)Math.Ceiling((double)result.Count() / 3);
            }

            result = result.Skip((aktifSayfa - 1) * 3).Take(3);

            return result.ToList();
        }
    }
}

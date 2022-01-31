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
    }
}

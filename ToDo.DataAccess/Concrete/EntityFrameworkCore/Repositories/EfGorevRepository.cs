using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGorevRepository : EfGenericRepository<Gorev>, IGorevDal
    {
        public Gorev GetirAciliyetIleId(int id)
        {
            using TodoContext context = new();
            return context.Gorevler
                  .Include(x => x.Aciliyet)
                  .FirstOrDefault(x => x.Durum == false && x.Id == id);
        }

        public List<Gorev> GetirAciliyetIleTamamlanmayanlari()
        {
            using TodoContext context = new();
            return context.Gorevler
                  .Include(x => x.Aciliyet)
                  .Where(x => x.Durum == false)
                  .OrderByDescending(x => x.OlusturmaTarihi)
                  .ToList();
        }

        public Gorev GetirRaporlarIleId(int id)
        {
            using TodoContext context = new();
            return context.Gorevler
                .Include(x => x.Raporlar)
                .Include(x => x.AppUser)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Gorev> GetirAppUserId(int appUserId)
        {
            using TodoContext context = new();
            return context.Gorevler
                .Where(x => x.AppUserId == appUserId)
                .ToList();
        }

        public List<Gorev> GetirTumTablolarla()
        {
            using TodoContext context = new();
            return context.Gorevler
                  .Include(x => x.Aciliyet)
                  .Include(x => x.Raporlar)
                  .Include(x => x.AppUser)
                  .Where(x => x.Durum == false)
                  .OrderByDescending(x => x.OlusturmaTarihi)
                  .ToList();
        }

        public List<Gorev> GetirTumTablolarla(Expression<Func<Gorev, bool>> filter)
        {
            using TodoContext context = new();
            return context.Gorevler
                  .Include(x => x.Aciliyet)
                  .Include(x => x.Raporlar)
                  .Include(x => x.AppUser)
                  .Where(filter)
                  .OrderByDescending(x => x.OlusturmaTarihi)
                  .ToList();
        }

        public List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa = 1)
        {
            using TodoContext context = new();
            var returnValue = context.Gorevler
                              .Include(x => x.Aciliyet)
                              .Include(x => x.Raporlar)
                              .Include(x => x.AppUser)
                              .Where(x => x.AppUserId == userId && x.Durum)
                              .OrderByDescending(x => x.OlusturmaTarihi);

            toplamSayfa = (int)Math
                .Ceiling((double)returnValue
                .Count() / 3);

            return returnValue
                .Skip((aktifSayfa - 1) * 3)
                .Take(3)
                .ToList();
        }

        public int GetirGorevSayisiTamamlananileAppUserId(int id)
        {
            using TodoContext context = new();
            return context.Gorevler
                .Count(I => I.AppUserId == id && I.Durum);
        }

        public int GetirGorevSayisiTamamlanmasiGerekenileAppUserId(int id)
        {
            using TodoContext context = new();
            return context.Gorevler
                .Count(I => I.AppUserId == id && !I.Durum);
        }

        public int GetirGorevSayisiAtanmayiBekleyen()
        {
            using TodoContext context = new();
            return context.Gorevler
                .Count(I => I.AppUserId == null && !I.Durum);
        }

        public int GetirGorevTamamlanmis()
        {
            using TodoContext context = new();
            return context.Gorevler
                .Count(I => I.Durum);
        }

        public int GetirGorevSayisiTamamlananileAppUserId(int id)
        {
            using TodoContext context = new();
            return context.Gorevler
                .Count(I => I.AppUserId == id && I.Durum);
        }

        public int GetirGorevSayisiTamamlanmasiGerekenileAppUserId(int id)
        {
            using TodoContext context = new();
            return context.Gorevler
                .Count(I => I.AppUserId == id && !I.Durum);
        }
    }
}

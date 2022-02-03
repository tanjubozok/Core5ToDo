using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Business.Interfaces;
using ToDo.Entities.Concrete;
using ToDo.WebUI.Areas.Admin.Models;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class IsEmriController : Controller
    {
        private readonly IAppUserService _userService;
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDosyaService _dosyaService;

        public IsEmriController(IAppUserService userService, IGorevService gorevService, UserManager<AppUser> userManager, IDosyaService dosyaService)
        {
            _userService = userService;
            _gorevService = gorevService;
            _userManager = userManager;
            _dosyaService = dosyaService;
        }

        public IActionResult ListeIsEmri()
        {
            TempData["Active"] = "isemri";

            var gorevler = _gorevService.GetirTumTablolarla();
            List<GorevListAllViewModel> modelList = new();

            foreach (var item in gorevler)
            {
                GorevListAllViewModel model = new()
                {
                    Id = item.Id,
                    Aciklama = item.Aciklama,
                    Aciliyet = item.Aciliyet,
                    Ad = item.Ad,
                    AppUser = item.AppUser,
                    OluşturmaTarihi = item.OlusturmaTarihi,
                    Raporlar = item.Raporlar
                };
                modelList.Add(model);
            }
            return View(modelList);
        }

        public IActionResult EklePersonelGorev(int id, string s, int sayfa = 1)
        {
            TempData["Active"] = "isemri";

            var gorev = _gorevService.GetirAciliyetIleId(id);

            ViewBag.AktifSayfa = sayfa;
            ViewBag.Aranan = s;

            int toplamSayfa;
            var personeller = _userService.GetirAdminOlmayanlar(out toplamSayfa, s, sayfa);
            ViewBag.ToplamSayfa = toplamSayfa;

            List<AppUserListViewModel> userListModel = new();
            foreach (var item in personeller)
            {
                AppUserListViewModel userModel = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    SurName = item.SurName,
                    Email = item.Email,
                    Picture = item.Picture
                };
                userListModel.Add(userModel);
            }
            ViewBag.Personeller = userListModel;

            GorevListViewModel gorevModel = new()
            {
                Id = gorev.Id,
                Ad = gorev.Ad,
                Aciklama = gorev.Aciklama,
                Aciliyet = gorev.Aciliyet,
                OlusturmaTarihi = gorev.OlusturmaTarihi
            };
            return View(gorevModel);
        }

        [HttpPost]
        public IActionResult EklePersonelGorev(PersonelGorevlendirViewModel model)
        {
            var gorev = _gorevService.GetirId(model.GorevId);
            gorev.AppUserId = model.PersonelId;
            _gorevService.Guncelle(gorev);
            return RedirectToAction("ListeIsEmri");
        }

        public IActionResult GorevlendirPersonel(PersonelGorevlendirViewModel model)
        {
            TempData["Active"] = "isemri";

            var user = _userManager.Users.FirstOrDefault(x => x.Id == model.PersonelId);
            var gorev = _gorevService.GetirAciliyetIleId(model.GorevId);

            AppUserListViewModel userModel = new()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                SurName = user.SurName,
                Picture = user.Picture
            };

            GorevListViewModel gorevModel = new()
            {
                Id = gorev.Id,
                Ad = gorev.Ad,
                Aciklama = gorev.Aciklama,
                Aciliyet = gorev.Aciliyet,
                OlusturmaTarihi = gorev.OlusturmaTarihi
            };

            PersonelGorevlendirListViewModel viewModel = new()
            {
                AppUser = userModel,
                Gorev = gorevModel
            };

            return View(viewModel);
        }

        public IActionResult GorevPersonelDetay(int id)
        {
            TempData["Active"] = "isemri";

            var gorev = _gorevService.GetirRaporlarIleId(id);
            GorevListAllViewModel model = new()
            {
                Id = gorev.Id,
                Ad = gorev.Ad,
                Raporlar = gorev.Raporlar,
                Aciklama = gorev.Aciklama,
                AppUser = gorev.AppUser
            };
            return View(model);
        }

        public IActionResult GetirExcel(int id)
        {
            var list = _gorevService.GetirRaporlarIleId(id).Raporlar;
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileDownloadName = Guid.NewGuid() + ".xlsx";

            return File(_dosyaService.AktarExcel(list), contentType, fileDownloadName);
        }

        public IActionResult GetirPdf(int id)
        {
            var list = _gorevService.GetirRaporlarIleId(id).Raporlar;
            const string contentType = "application/pdf";
            string fileDownloadName = Guid.NewGuid() + ".pdf";

            return File(_dosyaService.AktarPdf(list), contentType, fileDownloadName);
        }
    }
}

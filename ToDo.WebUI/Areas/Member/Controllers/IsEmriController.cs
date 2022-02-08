using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Interfaces;
using ToDo.Entities.Concrete;
using ToDo.WebUI.Areas.Admin.Models;
using ToDo.WebUI.Areas.Member.Models;

namespace ToDo.WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    public class IsEmriController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly IRaporService _raporService;
        private readonly UserManager<AppUser> _userManager;

        public IsEmriController(IGorevService gorevService, UserManager<AppUser> userManager, IRaporService raporService)
        {
            _gorevService = gorevService;
            _raporService = raporService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "isemri";

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var gorevler = _gorevService.GetirTumTablolarla(x => x.AppUserId == user.Id && !x.Durum);
            List<GorevListAllViewModel> listModel = new();

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
                listModel.Add(model);
            }
            return View(listModel);
        }

        public IActionResult RaporYaz(int id)
        {
            TempData["Active"] = "isemri";

            var gorev = _gorevService.GetirAciliyetIleId(id);
            RaporAddViewModel model = new()
            {
                GorevId = id,
                Gorev = gorev
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult RaporYaz(RaporAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                Rapor rapor = new()
                {
                    Detay = model.Detay,
                    Tanim = model.Tanim,
                    GorevId = model.GorevId
                };
                _raporService.Kaydet(rapor);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult RaporDuzenle(int id)
        {
            var rapor = _raporService.GetirGorevIleId(id);
            RaporGuncelleViewModel model = new()
            {
                Id = id,
                Tanim = rapor.Tanim,
                Detay = rapor.Detay,
                Gorev = rapor.Gorev
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult RaporDuzenle(RaporGuncelleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rapor = _raporService.GetirGorevIleId(model.Id);
                rapor.Tanim = model.Tanim;
                rapor.Detay = model.Detay;

                _raporService.Guncelle(rapor);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult TamamlaGorev(int gorevId)
        {
            var gorev = _gorevService.GetirId(gorevId);
            gorev.Durum = true;
            _gorevService.Guncelle(gorev);
            return Json(null);
        }
    }
}

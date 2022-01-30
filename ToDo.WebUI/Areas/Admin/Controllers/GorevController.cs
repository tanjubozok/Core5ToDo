using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.Entities.Concrete;
using ToDo.WebUI.Areas.Admin.Models;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly IAciliyetService _aciliyetService;

        public GorevController(IGorevService gorevService, IAciliyetService aciliyetService)
        {
            _gorevService = gorevService;
            _aciliyetService = aciliyetService;
        }

        public IActionResult ListeGorev()
        {
            TempData["Active"] = "gorev";

            var gorevler = _gorevService.GetirAciliyetIleTamamlanmayanlari();
            List<GorevListViewModel> listModel = new();
            foreach (var item in gorevler)
            {
                GorevListViewModel model = new()
                {
                    Aciklama = item.Aciklama,
                    Aciliyet = item.Aciliyet,
                    AciliyetId = item.AciliyetId,
                    Ad = item.Ad,
                    Durum = item.Durum,
                    Id = item.Id,
                    OlusturmaTarihi = item.OlusturmaTarihi
                };
                listModel.Add(model);
            }
            return View(listModel);
        }

        public IActionResult EkleGorev()
        {
            TempData["Active"] = "gorev";

            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim");
            return View();
        }

        [HttpPost]
        public IActionResult EkleGorev(GorevEkleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Gorev gorev = new()
                {
                    Ad = model.Ad,
                    Aciklama = model.Aciklama,
                    AciliyetId = model.AciliyetId
                };
                _gorevService.Kaydet(gorev);
                return RedirectToAction("ListeGorev");
            }
            return View(model);
        }

        public IActionResult GuncelleGorev(int id)
        {
            TempData["Active"] = "gorev";

            var gorev = _gorevService.GetirId(id);
            var model = new GorevGuncelleViewModel
            {
                Id = id,
                Ad = gorev.Ad,
                Aciklama = gorev.Aciklama,
                AciliyetId = gorev.AciliyetId
            };
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim", gorev.AciliyetId);
            return View(model);
        }

        [HttpPost]
        public IActionResult GuncelleGorev(GorevGuncelleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var gorev = _gorevService.GetirId(model.Id);
                gorev.Ad = model.Ad;
                gorev.Aciklama = model.Aciklama;
                gorev.AciliyetId = model.AciliyetId;
                _gorevService.Guncelle(gorev);
                return RedirectToAction("ListeGorev");
            }
            return View(model);
        }

        public IActionResult GorevSil(int id)
        {
            var gorev = _gorevService.GetirId(id);
            if (gorev != null)
            {
                _gorevService.Sil(gorev);
                return Json(null);
            }
            return View();
        }
    }
}

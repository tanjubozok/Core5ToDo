using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.Entities.Concrete;
using ToDo.WebUI.Areas.Admin.Models;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AciliyetController : Controller
    {
        private readonly IAciliyetService _aciliyetService;

        public AciliyetController(IAciliyetService aciliyetService)
        {
            _aciliyetService = aciliyetService;
        }

        public IActionResult ListeAciliyet()
        {
            TempData["Active"] = "aciliyet";

            var aciliyetler = _aciliyetService.GetirHepsi();
            var model = new List<AciliyetListViewModel>();

            foreach (var item in aciliyetler)
            {
                var aciliyetModel = new AciliyetListViewModel()
                {
                    Id = item.Id,
                    Tanim = item.Tanim
                };
                model.Add(aciliyetModel);
            }
            return View(model);
        }

        public IActionResult EkleAciliyet()
        {
            TempData["Active"] = "aciliyet";

            return View();
        }

        [HttpPost]
        public IActionResult EkleAciliyet(AciliyetEkleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Aciliyet aciliyet = new()
                {
                    Tanim = model.Tanim
                };
                _aciliyetService.Kaydet(aciliyet);

                return RedirectToAction("ListeAciliyet");
            }
            return View(model);
        }

        public IActionResult GuncelleAciliyet(int id)
        {
            TempData["Active"] = "aciliyet";

            var aciliyet = _aciliyetService.GetirId(id);
            var model = new AciliyetGuncelleViewModel
            {
                Id = id,
                Tanim = aciliyet.Tanim
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult GuncelleAciliyet(AciliyetGuncelleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var aciliyet = _aciliyetService.GetirId(model.Id);
                aciliyet.Tanim = model.Tanim;
                _aciliyetService.Guncelle(aciliyet);
                return RedirectToAction("ListeAciliyet");
            }
            return View(model);
        }
    }
}

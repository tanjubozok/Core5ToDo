using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.WebUI.Areas.Admin.Models;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;

        public GorevController(IGorevService gorevService)
        {
            _gorevService = gorevService;
        }

        public IActionResult ListeGorev()
        {
            TempData["Active"] = "gorev";

            var gorevler = _gorevService.GetirHepsi();
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
    }
}

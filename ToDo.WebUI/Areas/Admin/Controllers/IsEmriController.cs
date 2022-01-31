using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.WebUI.Areas.Admin.Models;

namespace ToDo.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class IsEmriController : Controller
    {
        private readonly IAppUserService _userService;
        private readonly IGorevService _gorevService;

        public IsEmriController(IAppUserService userService, IGorevService gorevService)
        {
            _userService = userService;
            _gorevService = gorevService;
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
    }
}

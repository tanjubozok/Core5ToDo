using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Interfaces;
using ToDo.Entities.Concrete;
using ToDo.WebUI.Areas.Admin.Models;

namespace ToDo.WebUI.Areas.Member.Controllers
{
    [Authorize(Roles = "Member")]
    [Area("Member")]
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;

        public GorevController(IGorevService gorevService, UserManager<AppUser> userManager)
        {
            _gorevService = gorevService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int aktifSayfa = 1)
        {
            TempData["Active"] = "gorev";

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var gorevler = _gorevService.GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, user.Id, aktifSayfa);

            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.AktifSayfa = aktifSayfa;

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
    }
}
